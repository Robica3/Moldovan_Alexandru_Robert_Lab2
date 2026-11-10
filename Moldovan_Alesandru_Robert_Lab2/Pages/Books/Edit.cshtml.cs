using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Moldovan_Alexandru_Robert_Lab2.Models;

namespace Moldovan_Alexandru_Robert_Lab2.Pages.BookPages;

public class EditModel : BookCategoriesPageModel
{
    private readonly Moldovan_Alexandru_Robert_Lab2Context _context;

    public EditModel(Moldovan_Alexandru_Robert_Lab2Context context)
    {
        _context = context;
    }

    [BindProperty]
    public Book Book { get; set; } = default!;

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        if (id is null)
        {
            return NotFound();
        }

        var book = await _context.Book
            .Include(ba => ba.Author)
            .Include(b => b.Publisher)
            .Include(b=>b.BookCategories)
            .ThenInclude(bc=>bc.Category)
            .FirstOrDefaultAsync(m => m.ID == id);
        if (book is null)
        {
            return NotFound();
        }
        Book = book;
        PopulateAssignedCategoryData(_context, Book);

        ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
        return Page();
    }

    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see https://aka.ms/RazorPagesCRUD.
    public async Task<IActionResult> OnPostAsync(int? id, string[] selectedCategories)
    {
        if (id == null)
        {
            return NotFound();
        }
        var bookToUpdate = await _context.Book
            .Include(btu => btu.Author)
            .Include(btu => btu.Publisher)
            .Include(btu => btu.BookCategories)
            .ThenInclude(btuc => btuc.Category)

           .FirstOrDefaultAsync(m => m.ID == id);
        if (bookToUpdate is null) { return NotFound();
        }

        if (await TryUpdateModelAsync<Book>(bookToUpdate, "Book",
            b => b.Title, b => b.AuthorID, b => b.Price, b => b.PublishingDate, b => b.PublisherID))
        {
            UpdateBookCategories(_context, bookToUpdate, selectedCategories);
            await _context.SaveChangesAsync();
            return RedirectToPage("./Index");
        }

        UpdateBookCategories(_context, bookToUpdate, selectedCategories);
        PopulateAssignedCategoryData(_context, bookToUpdate);
        ViewData["AuthorID"] = new SelectList(_context.Author, "ID", "FullName");
        ViewData["PublisherID"] = new SelectList(_context.Publisher, "ID", "PublisherName");
        return Page();


    }
    

    private bool BookExists(int id)
    {
        return _context.Book.Any(e => e.ID == id);
    }
}
