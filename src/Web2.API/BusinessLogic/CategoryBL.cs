﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using Web2.API.Models;

namespace Web2.API.BusinessLogic
{
    public class CategoryBL : ICategoryBL
    {
        public Category Add(Category value)
        {
            if (value is null || String.IsNullOrEmpty(value.Name?.Trim()) )
            {
                throw new HttpException 
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis"},
                    StatusCode = StatusCodes.Status400BadRequest 
                };
            }

            var category = new Category { Name = value.Name.Trim().ToUpper() };
            category.ID = Repository.IdSequenceCategory++;
            Repository.Categories.Add(category);

            return category;
        }

        public void Delete(int id)
        {
            var category = Repository.Categories.FirstOrDefault(x => x.ID == id);
            if (category != null)
            {
                if (Repository.Evenements.Any(x => x.CategoryIDs.Contains(category.ID)))
                {
                    throw new HttpException
                    {
                        Errors = new { Error = "Il n'est pas possible de supprimer une categorie lié a au moins un evenement" },
                        StatusCode = StatusCodes.Status409Conflict
                    };

                }
                Repository.Categories.Remove(category);
            }
        }

        public Category Get(int id)
        {
            return Repository.Categories.FirstOrDefault(x => x.ID == id);
        }

        public IEnumerable<Category> GetList()
        {
            return Repository.Categories;
        }

        public Category Updade(int id, Category value)
        {
            if (value == null || String.IsNullOrEmpty(value.Name?.Trim()))
            {
                throw new HttpException
                {
                    Errors = new { Error = "Une valeur pour le champs 'Nom' est requis" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            var category = Repository.Categories.FirstOrDefault(x => x.ID == id);


            if (category == null)
            {
                throw new HttpException
                {
                    Errors = new { Errors = $"Element introuvable (id = {id})" },
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }

            category.Name = value.Name.Trim().ToUpper();

            return category;
        }
    }
}