﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BusinessObject.DatabaseContext;
using BusinessObject.Models;
using DataAccess.Repositories;

namespace PRN221ExampleWeb.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly UserRepository _userRepository;

        public IndexModel(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ICollection<User> User { get;set; } = default!;
        [BindProperty]
        public string search { get; set; }

        public async Task OnGetAsync()
        {
            User = _userRepository.GetUsersList();
        }
        public async Task OnPostAsync()
        {
            User = _userRepository.SearchByName(search);
        }
    }
}
