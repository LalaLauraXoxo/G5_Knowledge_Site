﻿using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASI.Basecode.Services.Interfaces
{
    public interface ICategoryService
    {
        public void AddCategory(CategoryViewModel categoryViewModel, string username);

        public List<Category> GetCategory();

        public Category GetCategory(int id);

        public bool UpdateCategory(CategoryViewModel categoryViewModel, string username);

        public bool DeleteCategory(CategoryViewModel categoryViewModel);
    }
}
