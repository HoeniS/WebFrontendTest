﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.Fahrzeuge.Autos
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovie.Models.AuthenticationContext _context;

        public IndexModel(RazorPagesMovie.Models.AuthenticationContext context)
        {
            _context = context;
        }

        public IList<Auto> Auto { get;set; }

        public async Task OnGetAsync()
        {
            Auto = await _context.Auto.ToListAsync();

            foreach (var fahrzeug in Auto)
            {
                if (fahrzeug.AusgeliehenUM.AddMinutes(fahrzeug.Ausleihzeit) <= DateTime.Now)
                {
                    fahrzeug.Verfuegbar = true;
                    fahrzeug.Kundenname = null;
                }
            }
        }
    }
}
