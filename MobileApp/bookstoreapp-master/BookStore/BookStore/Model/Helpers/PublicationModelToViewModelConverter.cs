using BookStore.Helpers;
using BookStore.Service;
using BookStore.ViewModel;

namespace BookStore.Model.Helpers
{
    public static class PublicationModelToViewModelConverter
    {
        public static PublicationViewModel Convert(Publication publication)
        {
            if (publication is Book book)
            {
                return new BookViewModel(book)
                {
                    PublishedDate = UnixTimestampConverter.UnixTimestampToDateTime(book.PublishedDate)
                };
            }
            if (publication is Journal journal)
            {
                return new JournalViewModel(journal)
                {
                    PublishedDate = UnixTimestampConverter.UnixTimestampToDateTime(journal.PublishedDate),
                    FirstPublishedDate = UnixTimestampConverter.UnixTimestampToDateTime(journal.FirstPublishedDate),
                    Recurrence = RecurrenceConverter.StringToRecurrence(journal.Recurrence),
                    ConferencePapers = journal.ConferencePapers.ConvertAll(id => Convert(PublicationService.Instance.GetById(id)) as ConferencePaperViewModel)
                };
            }
            if (publication is ConferencePaper conferencePaper)
            {
                return new ConferencePaperViewModel(conferencePaper)
                {
                    PublishedDate = UnixTimestampConverter.UnixTimestampToDateTime(conferencePaper.PublishedDate)
                };
            }

            return null;
        }
    }
}
