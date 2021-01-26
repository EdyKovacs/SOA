using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Model;
using Xamarin.Forms;

namespace BookStore.Service
{
    public class PublicationService
    {
        private static PublicationService _instance;
        public static PublicationService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new PublicationService();
                }

                return _instance;
            }
        }
        public List<Publication> Publications { get; private set; } = new List<Publication>();

        private PublicationService()
        {
        }

        public void AddPublication(Publication publicationToAdd)
        {
            publicationToAdd.CoverImageSource = SetPublicationCover();
            Publications.Add(publicationToAdd);
        }

        public void DeletePublication(Publication publicationToDelete)
        {
            Publications.Remove(publicationToDelete);
        }

        public void UpdatePublication(Publication updatedPublication)
        {
            var publicationToUpdate = GetById(updatedPublication.Id);
            DeletePublication(publicationToUpdate);
            AddPublication(updatedPublication);
        }

        public async Task<List<Publication>> FetchAllPublicationsAsync()
        {
            Publications = await RestCrudOperationsService.Instance.GetPublicationsAsync();
            Publications.ForEach(publication => publication.CoverImageSource = SetPublicationCover());

            return Publications;
        }

        public List<Publication> GetAllPublications()
        {
            return Publications;
        }

        public Publication GetById(string publicationId)
        {
            return Publications.First(publication => publication.Id.Equals(publicationId));
        }

        public List<ConferencePaper> GetAllConferencePapers()
        {
            return Publications.Where(publication => publication is ConferencePaper).ToList().ConvertAll(publication => publication as ConferencePaper);
        }

        public ImageSource SetPublicationCover()
        {
            Random random = new Random();
            int randomImageId = random.Next(1, 1084);

            return ImageSource.FromUri(new Uri($"https://picsum.photos/id/{randomImageId}/328/400"));
        }
    }
}
