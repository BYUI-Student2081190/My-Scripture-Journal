using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using My_Scripture_Journal.Data;
using My_Scripture_Journal.Models;

namespace My_Scripture_Journal.Pages.Scriptures
{
    public class IndexModel : PageModel
    {
        private readonly My_Scripture_Journal.Data.My_Scripture_JournalContext _context;

        public IndexModel(My_Scripture_Journal.Data.My_Scripture_JournalContext context)
        {
            _context = context;
        }

        public IList<Scripture> Scripture { get;set; } = default!;

        [BindProperty(SupportsGet = true)]
        public string? searchString { get; set; }

        public SelectList? Books { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? BookName { get; set; }

        public SelectList? Dates { get; set; }


        [BindProperty(SupportsGet = true)]
        public DateTime? ObjectDate {get; set;}


        public async Task OnGetAsync()
        {
            // Add the search.
            // Uses System.Linq;
            IQueryable<string> bookQuery = from s in _context.Scripture
                                           orderby s.Book
                                           select s.Book;

            IQueryable<DateTime> dateQuery = from s in _context.Scripture
                                             orderby s.PublishDate.Date
                                             select s.PublishDate.Date;

            var scriptures = from s in _context.Scripture
                             select s;

            // Check to see if the searchString is not empty.
            if (!string.IsNullOrEmpty(searchString) ) 
            {
                scriptures = scriptures.Where(s => s.Notes.Contains(searchString));
            }

            if (!string.IsNullOrEmpty(BookName)) 
            {
                scriptures = scriptures.Where(x => x.Book == BookName);
            }

            if (ObjectDate != null) 
            {
                scriptures = scriptures.Where(x => x.PublishDate.Date == ObjectDate);
            }

            // Create list of data on page.
            Books = new SelectList(await bookQuery.Distinct().ToListAsync());
            Dates = new SelectList(await dateQuery.Distinct().ToListAsync());
            Scripture = await scriptures.ToListAsync();
        }
    }
}
