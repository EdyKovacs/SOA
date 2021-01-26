using System;
using System.IO;
using System.Threading.Tasks;
using BookStore.Helpers;
using BookStore.Service;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace BookStore.CustomViews
{
    public partial class ImagePicker : Frame
    {
        #region Bindable Properties

        public static readonly BindableProperty IsUploadedProperty =
            BindableProperty.Create(nameof(IsUploaded), typeof(bool), typeof(ImagePicker), false, propertyChanged: HandleIsUploadedChanged, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty UploadTextProperty =
            BindableProperty.Create(nameof(UploadText), typeof(string), typeof(ImagePicker), Constants.StandardStringConstants.UploadImageString.Value, propertyChanged: UploadTextPropertyChanged);

        public static readonly BindableProperty RemoveTextProperty =
            BindableProperty.Create(nameof(RemoveText), typeof(string), typeof(ImagePicker), Constants.StandardStringConstants.RemoveImageString.Value);

        public static readonly BindableProperty ImagePathProperty =
            BindableProperty.Create(nameof(ImagePath), typeof(ImageSource), typeof(ImagePicker), null, propertyChanged: ImagePathPropertyChanged);

        #endregion

        #region Properties

        public bool IsUploaded
        {
            get => (bool)GetValue(IsUploadedProperty);
            set => SetValue(IsUploadedProperty, value);
        }

        public string UploadText
        {
            get => (string)GetValue(UploadTextProperty);
            set => SetValue(UploadTextProperty, value);
        }

        public string RemoveText
        {
            get => (string)GetValue(RemoveTextProperty);
            set => SetValue(RemoveTextProperty, value);
        }

        public ImageSource ImagePath
        {
            get => (ImageSource)GetValue(ImagePathProperty);
            set => SetValue(ImagePathProperty, value);
        }
        #endregion

        public ImagePicker()
        {
            InitializeComponent();

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(async () =>
                {
                    if(IsUploaded == false)
                    {
                        await CrossMedia.Current.Initialize();
                        MediaFile file = null;

                        string action = await PageService.Instance.DisplayActionSheetAsync(null,
                            Constants.StandardStringConstants.CancelString.Value,
                            Constants.StandardStringConstants.TakePhotoString.Value,
                            Constants.StandardStringConstants.ChoosePhotoString.Value);
                        if(action == null)
                        {
                            return;
                        }

                        if (action.Equals(Constants.StandardStringConstants.TakePhotoString.Value))
                        {
                            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                            {
                                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.NoCameraAvailableErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                                return;
                            }
                            try
                            {
                                file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                                {
                                    Directory = "Sample",
                                    Name = "test.jpg"
                                });
                            }
                            catch(Exception)
                            {
                                await HandleMissingPermissionExceptionAsync();
                            }
                        }
                        else if (action.Equals(Constants.StandardStringConstants.ChoosePhotoString.Value))
                        {
                            if (!CrossMedia.Current.IsPickPhotoSupported)
                            {
                                await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.NotSupportedErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
                                return;
                            }

                            try
                            {
                                file = await CrossMedia.Current.PickPhotoAsync();

                            }
                            catch (Exception)
                            {
                                await HandleMissingPermissionExceptionAsync();
                            }
                        }

                        if (file == null)
                        {
                            return;
                        }

                        ImagePath = ImageSource.FromFile(file.Path);
                        IsUploaded = true;
                    }
                })
            });
        }

        void OnRemoveButtonClicked(object sender, EventArgs e)
        {
            if (IsUploaded == true)
            {
                ImagePath = null;
                IsUploaded = false;
            }
        }

        async Task HandleMissingPermissionExceptionAsync()
        {
            await PageService.Instance.DisplayAlertAsync(Constants.ValidatorStrings.StandardErrorMessage.Value, Constants.ValidatorStrings.PermissionsErrorMessage.Value, Constants.StandardStringConstants.OkString.Value);
        }

        #region Property Changed Handlers

        private static void HandleIsUploadedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var picker = bindable as ImagePicker;
            var isUploaded = (bool)newValue;

            picker.Container.Orientation = isUploaded ? StackOrientation.Horizontal : StackOrientation.Vertical;
            picker.InnerIcon.Source = isUploaded ? Constants.IconSources.CoverIconSource.Value : Constants.IconSources.UploadIconSource.Value;
            picker.InnerLabel.Text = isUploaded ? picker.RemoveText : picker.UploadText ;
            picker.RemoveButton.IsVisible = isUploaded;
            picker.PreviewImage.Source = picker.ImagePath;
            picker.PreviewImage.IsVisible = isUploaded;
        }

        private static void UploadTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var picker = bindable as ImagePicker;
            picker.InnerLabel.Text = (string)newValue;
        }

        private static void ImagePathPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue == null || oldValue.Equals(newValue))
            {
                return;
            }

            var picker = bindable as ImagePicker;
            picker.ImagePath = (ImageSource)newValue;
        }

        #endregion
    }
}
