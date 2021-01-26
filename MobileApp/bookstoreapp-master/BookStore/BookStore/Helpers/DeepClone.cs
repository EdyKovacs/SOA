using System;
using BookStore.ViewModel;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookStore.Helpers
{
    public static class DeepClone
    {
        public static T CloneJson<T>(this T source)
        {
            if (Object.ReferenceEquals(source, null))
            {
                return default;
            }

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new PublicationConverter<PublicationViewModel, BookViewModel>());
            settings.Converters.Add(new PublicationConverter<PublicationViewModel, JournalViewModel>());
            settings.Converters.Add(new PublicationConverter<PublicationViewModel, ConferencePaperViewModel>());
            settings.Converters.Add(new PublicationConverter<ImageSource, UriImageSource>());
            settings.Converters.Add(new PublicationConverter<ImageSource, FileImageSource>());

            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), settings);
        }
    }
}
