using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using SqlDataMapper;

using LTB_Database.Core;
using LTB_Database.Core.DataAccess;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;
using LTB_Database.ViewModels;

namespace LTB_Database.Core
{
	public class CategoryService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(CategoryService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;

		public CategoryService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);
			_validatonDictionary = validationDictionary;	
		}
		
		public BookModel[] GetBooks()
		{
			BookRepository bRepo = new BookRepository(_dal);

			Book[] books = bRepo.List();

			List<BookModel> bookModel = new List<BookModel>();
			foreach(Book book in books)
			{
				bookModel.Add(new BookModel
				{
					Id = book.id,
					Name = book.name,
					Number = book.number,
					CategoryId = book.catid,
					Image = book.image,
					Added = book.added
				});
			}
			
			return bookModel.ToArray();
		}
		
		public CategoryModel GetCategory(long id)
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);
			
			Category category = cRepo.Get(id);
			if(category == null)
				throw new HttpException(404, "Ressource not found");
			
			return new CategoryModel
			{
				Id = category.id,
				Name = category.name
			};
		}

		public CategoryModel[] GetCategories()
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);

			Category[] categories = cRepo.List();

			List<CategoryModel> categorieModel = new List<CategoryModel>();
			foreach (Category categorie in categories)
			{
				categorieModel.Add(new CategoryModel
				{
					Id = categorie.id,
					Name = categorie.name
				});
			}

			return categorieModel.ToArray();
		}
		
		public long SaveCategory(CategoryModel model)
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);
			
			Category category = new Category
			{
				id = model.Id,
				name = model.Name.Escape().Trim()
			};
			
			if(category.id == 0)
			{
				try
				{
					_dal.BeginTransaction();
					
					long ret = cRepo.Insert(category);
					if (ret == 0)
						throw new Exception("Category was not inserted.");
						
					long id = cRepo.GetLastInsertId();
					
					_dal.CommitTransaction();
						
					return id;
				}
				catch(Exception ex)
				{
					_dal.RollbackTransaction();
					log.Error(ex);
					throw;
				}
			}
			else
			{
				try
				{
					_dal.BeginTransaction();
				
					long ret = cRepo.Update(category);
					if (ret == 0)
						throw new Exception("Category was not updated.");
					
					_dal.CommitTransaction();
					
					return model.Id;
				}
				catch(Exception ex)
				{
					_dal.RollbackTransaction();
					log.Error(ex);
					throw;
				}
			}
		}
		
		public void DeleteCategory(long id)
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);
			BookRepository bRepo = new BookRepository(_dal);
			
			Category category = cRepo.Get(id);
			if(category == null)
				throw new HttpException(404, "Ressource not found.");
			
			try
			{
				_dal.BeginTransaction();
				
				long ret = cRepo.Delete(id);
				if(ret == 0)
					throw new Exception("No category was deleted.");
					
				_dal.CommitTransaction();
			}
			catch(Exception ex)
			{
				_dal.RollbackTransaction();
				log.Error(ex);
				throw;
			}
		}
	}
}