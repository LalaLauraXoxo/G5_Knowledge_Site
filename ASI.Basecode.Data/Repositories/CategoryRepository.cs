﻿using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class CategoryRepository : BaseRepository, ICategoryRepository
    {

        public CategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }
        public bool CategoryExists(string CategoryName)
        {
            return this.GetDbSet<Category>().Any(x => x.CategoryName == CategoryName);
        }
        public void AddCategory(Category category)
        {
            this.GetDbSet<Category>().Add(category);
            UnitOfWork.SaveChanges();
        }

        //get all categories
        public List<Category> GetCategories()
        {
            var category = GetDbSet<Category>().ToList();
            return category;
        }

        //retrieve single book
        public Category GetCategory(int id)
        {
            var category = this.GetDbSet<Category>().FirstOrDefault(x => x.Id == id);
            return category;
        }

        public List<Category> GetCategorySelections()
        {
            return GetDbSet<Category>().ToList();
        }

        //update book
        public void UpdateCategory(Category category)
        {
            this.GetDbSet<Category>().Update(category);
            UnitOfWork.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            this.GetDbSet<Category>().Remove(category);
            UnitOfWork.SaveChanges();
        }

    }
}
