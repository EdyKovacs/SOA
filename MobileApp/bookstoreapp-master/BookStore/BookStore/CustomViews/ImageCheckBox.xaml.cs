using System.Windows.Input;
using BookStore.Helpers;
using Xamarin.Forms;

namespace BookStore.CustomViews
{
    public partial class ImageCheckBox : ContentView
    {
        #region Bindable Properties

        public static readonly BindableProperty IsActiveProperty =
            BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(ImageCheckBox), false, propertyChanged: HandleIsActiveChanged, defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty CheckBoxTextProperty =
            BindableProperty.Create(nameof(CheckBoxText), typeof(string), typeof(ImageCheckBox), string.Empty, propertyChanged: CheckBoxTextPropertyChanged);

        public static readonly BindableProperty DefaultImageSourceProperty =
            BindableProperty.Create(nameof(DefaultImageSource), typeof(ImageSource), typeof(ImageCheckBox), Constants.IconSources.CoverIconSource.Value, propertyChanged: DefaultImageChanged);

        public static readonly BindableProperty ActiveImageSourceProperty =
            BindableProperty.Create(nameof(ActiveImageSource), typeof(ImageSource), typeof(ImageCheckBox), Constants.IconSources.CoverIconSource.Value);

        public static readonly BindableProperty DefaultBackgroundColorProperty =
            BindableProperty.Create(nameof(DefaultBackgroundColor), typeof(Color), typeof(ImageCheckBox), Color.White, propertyChanged: DefaultBackgroundColorChanged);

        public static readonly BindableProperty ActiveBackgroundColorProperty =
            BindableProperty.Create(nameof(ActiveBackgroundColor), typeof(Color), typeof(ImageCheckBox), Color.GreenYellow);

        public static readonly BindableProperty DefaultTextColorProperty =
            BindableProperty.Create(nameof(DefaultTextColor), typeof(Color), typeof(ImageCheckBox), Color.Gray, propertyChanged: DefaultTextColorChanged);

        public static readonly BindableProperty ActiveTextColorProperty =
            BindableProperty.Create(nameof(ActiveTextColor), typeof(Color), typeof(ImageCheckBox), Color.Tomato);

        public static readonly BindableProperty DefaultFrameBorderColorProperty =
            BindableProperty.Create(nameof(DefaultFrameBorderColor), typeof(Color), typeof(ImageCheckBox), Color.Gainsboro, propertyChanged: DefaultFrameBoderColorChanged);

        public static readonly BindableProperty ActiveFrameBorderColorProperty =
            BindableProperty.Create(nameof(ActiveFrameBorderColor), typeof(Color), typeof(ImageCheckBox), Color.Tomato);

        public static readonly BindableProperty CheckBoxCommandProperty =
            BindableProperty.Create(nameof(CheckBoxCommand), typeof(Command), typeof(ImageCheckBox), null);

        public static readonly BindableProperty CheckBoxCommandParameterProperty =
            BindableProperty.Create(nameof(CheckBoxCommandParameter), typeof(object), typeof(ImageCheckBox), null);

        #endregion

        #region Properties

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public string CheckBoxText
        {
            get => (string)GetValue(CheckBoxTextProperty);
            set => SetValue(CheckBoxTextProperty, value);
        }

        public ImageSource DefaultImageSource
        {
            get => (ImageSource)GetValue(DefaultImageSourceProperty);
            set => SetValue(DefaultImageSourceProperty, value);
        }

        public ImageSource ActiveImageSource
        {
            get => (ImageSource)GetValue(ActiveImageSourceProperty);
            set => SetValue(ActiveImageSourceProperty, value);
        }

        public Color ActiveBackgroundColor
        {
            get => (Color)GetValue(ActiveBackgroundColorProperty);
            set => SetValue(ActiveBackgroundColorProperty, value);
        }

        public Color DefaultBackgroundColor
        {
            get => (Color)GetValue(DefaultBackgroundColorProperty);
            set => SetValue(DefaultBackgroundColorProperty, value);
        }

        public Color ActiveTextColor
        {
            get => (Color)GetValue(ActiveTextColorProperty);
            set => SetValue(ActiveTextColorProperty, value);
        }

        public Color DefaultTextColor
        {
            get => (Color)GetValue(DefaultTextColorProperty);
            set => SetValue(DefaultTextColorProperty, value);
        }

        public Color DefaultFrameBorderColor
        {
            get => (Color)GetValue(DefaultFrameBorderColorProperty);
            set => SetValue(DefaultFrameBorderColorProperty, value);
        }

        public Color ActiveFrameBorderColor
        {
            get => (Color)GetValue(ActiveFrameBorderColorProperty);
            set => SetValue(ActiveFrameBorderColorProperty, value);
        }

        public ICommand CheckBoxCommand
        {
            get => (Command)GetValue(CheckBoxCommandProperty);
            set => SetValue(CheckBoxCommandProperty, value);
        }

        public object CheckBoxCommandParameter
        {
            get => (object)GetValue(CheckBoxCommandParameterProperty);
            set => SetValue(CheckBoxCommandParameterProperty, value);
        }

        #endregion

        public ImageCheckBox()
        {
            InitializeComponent();

            this.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() =>
                {
                    IsActive = !IsActive;

                    if (CheckBoxCommand != null && CheckBoxCommand.CanExecute(CheckBoxCommandParameter))
                    {
                        CheckBoxCommand.Execute(CheckBoxCommandParameter);
                    }
                })
            });
        }

        #region Property Changed Handlers

        private static void HandleIsActiveChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            var isActive = (bool) newValue;
            checkBox.InnerImage.Source = isActive ? checkBox.ActiveImageSource : checkBox.DefaultImageSource;
            checkBox.InnerLabel.TextColor = isActive ? checkBox.ActiveTextColor : checkBox.DefaultTextColor;
            checkBox.MainFrame.BackgroundColor = isActive ? checkBox.ActiveBackgroundColor : checkBox.DefaultBackgroundColor;
            checkBox.MainFrame.BorderColor = isActive ? checkBox.ActiveFrameBorderColor : checkBox.DefaultFrameBorderColor;
        }

        private static void DefaultImageChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null ||  oldValue == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            checkBox.InnerImage.Source = (ImageSource) newValue;
        }

        private static void DefaultBackgroundColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            checkBox.MainFrame.BackgroundColor = (Color) newValue;
        }

        private static void DefaultTextColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            checkBox.InnerLabel.TextColor = (Color)newValue;
        }

        private static void DefaultFrameBoderColorChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            checkBox.MainFrame.BorderColor = (Color)newValue;
        }

        private static void CheckBoxTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable == null || oldValue.Equals(newValue))
            {
                return;
            }

            var checkBox = bindable as ImageCheckBox;
            checkBox.InnerLabel.Text = (string) newValue;
        }

        #endregion
    }
}
