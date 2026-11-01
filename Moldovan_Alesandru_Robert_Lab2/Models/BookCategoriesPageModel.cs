using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Globalization;

namespace Moldovan_Alexandru_Robert_Lab2.Models
{
    public class BookCategoriesPageModel : PageModel
    {
        public List<AssignedCategoryData> AssignedCategoryDataList;
        public void PopulateAssignedCategoryData(Moldovan_Alexandru_Robert_Lab2Context context, Book book)
        {
            var allCategories = context.Category;
            var bookCategories = new HashSet<int>(
                book.BookCategories.Select(bc => bc.CategoryID)
                );
            AssignedCategoryDataList = new List<AssignedCategoryData>();

            foreach (var cat in allCategories)
            {
                AssignedCategoryDataList.Add(new AssignedCategoryData
                {
                    CategoryID = cat.ID,
                    Name = cat.CategoryName,
                    Assigned = bookCategories.Contains(cat.ID),



                });
            }




        }
        public void UpdateBookCategories(Moldovan_Alexandru_Robert_Lab2Context context, Book bookToUpdate, string[] selectedCategories) {

            if (selectedCategories == null)
            {
                bookToUpdate.BookCategories = new List<BookCategory>();
                return;
            }
            var selectedCategoriesHS = new HashSet<string>(selectedCategories);
            var bookCategories = new HashSet<int>(
                bookToUpdate.BookCategories.Select(bc => bc.CategoryID)
                );
            foreach (var cat in context.Category)
            {
                if (selectedCategoriesHS.Contains(cat.ID.ToString()))
                {
                    if (!bookCategories.Contains(cat.ID))
                    {
                        bookToUpdate.BookCategories.Add(new BookCategory
                        {
                            BookID = bookToUpdate.ID,
                            CategoryID = cat.ID
                        });
                    }
                }
                else
                {
                    if (bookCategories.Contains(cat.ID))
                    {
                        if (bookCategories.Contains(cat.ID))
                        {
                            var bookToRemove = bookToUpdate.BookCategories
                                .SingleOrDefault(bc => bc.CategoryID == cat.ID);
                            context.Remove(bookToRemove);
                        }

                    }
                }
            }
        }
    }
}
