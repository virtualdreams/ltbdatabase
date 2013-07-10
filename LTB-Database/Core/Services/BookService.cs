using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using SqlDataMapper;

using LTB_Database.ViewModels;
using LTB_Database.Core.DataAccess;
using LTB_Database.Core.DataModel;
using LTB_Database.Models;
using System.Drawing;
using System.IO;

namespace LTB_Database.Core
{
	public class BookService
	{
		private static readonly log4net.ILog log = log4net.LogManager.GetLogger(typeof(BookService));
		private SqlMapper _dal = null;
		private IValidationDictionary _validatonDictionary = null;
		
		public BookService(IValidationDictionary validationDictionary)
		{
			_dal = new SqlMapper(GlobalConfig.Get().SqlMapperConfig);
			_validatonDictionary = validationDictionary;	
		}

		public BookModel GetBook(long id)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);

			Book book = bRepo.Get(id);
			if (book == null)
				throw new HttpException(404, "Ressource not found");

			Story[] stories = sRepo.List().Where(s => s.bookid == id).ToArray();
			List<string> storyNames = new List<string>();
			foreach (Story story in stories)
			{
				storyNames.Add(story.name);
			}

			BookModel bookModel = new BookModel()
			{
				Id = book.id,
				Name = book.name,
				Number = book.number,
				CategoryId = book.catid,
				Image = book.image,
				Added = book.added,
				Stories = storyNames.ToArray()
			};

			return bookModel;
		}

		public CategoryModel GetCategory(long id)
		{
			CategoryRepository cRepo = new CategoryRepository(_dal);

			Category category = cRepo.Get(id);
			if (category == null)
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

		public long SaveBook(BookModel model)
		{
			BookRepository bRepo = new BookRepository(_dal);
			StoryRepository sRepo = new StoryRepository(_dal);
			
			Book book = new Book
			{
				id = model.Id,
				name = model.Name.Escape().Trim(),
				number = model.Number,
				catid = model.CategoryId
			};

			if (model.Id == 0)
			{
				try
				{
					_dal.BeginTransaction();
					
					long ret = bRepo.Insert(book);
					if (ret == 0)
						throw new Exception("Book was not inserted.");
					
					long id = bRepo.GetLastInsertId();
					
					SaveImage(id, model.Imagefile);
					
					foreach(string storyName in model.Stories)
					{
						if(!String.IsNullOrEmpty(storyName.Trim()))
						{
							Story story = new Story
							{
								name = storyName.Escape().Trim(),
								bookid = id
							};
							
							long sret = sRepo.Insert(story);
							if(sret == 0)
								throw new Exception("Story was not inserted.");
						}
					}
					
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
					
					long ret = bRepo.Update(book);
					if (ret == 0)
						throw new Exception("Book was not updated.");
						
					SaveImage(model.Id, model.Imagefile);

					ret = sRepo.DeleteByBookId(model.Id);
					log.InfoFormat("Deleted {0} stories associated with book {1}", ret, model.Id);

					foreach (string storyName in model.Stories)
					{
						if (!String.IsNullOrEmpty(storyName.Trim()))
						{
							Story story = new Story
							{
								name = storyName.Escape().Trim(),
								bookid = model.Id
							};

							long sret = sRepo.Insert(story);
							if (sret == 0)
								throw new Exception("Story was not inserted.");
						}
					}
					
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
		
		private void SaveImage(long id, HttpPostedFileBase file)
		{
			if (file != null)
			{
				try
				{
					string[] allowedMimeTypes = GlobalConfig.Get().AllowMimeType.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
					if (allowedMimeTypes.Contains(file.ContentType.ToLower()))
					{
						Image image = Image.FromStream(file.InputStream);

						string imagePath = HttpContext.Current.Request.MapPath(GlobalConfig.Get().ImagePath);
						string imageName = String.Format("img_{0}.png", id);
						
						//save to file system
						image.Save(String.Format("{0}{1}", imagePath, imageName), System.Drawing.Imaging.ImageFormat.Png);
						
						//update the database
						BookRepository bRepo = new BookRepository(_dal);
						
						long ret = bRepo.SetImage(id, imageName);
						if(ret == 0)
							throw new Exception("Book was not updated");
					}
					else
					{
						throw new Exception("The image format is not allowed.");
					}
				}
				catch (Exception ex)
				{
					log.Fatal(ex);
					throw;
				}
			}
		}
		
		public void RemoveImage(long id)
		{
			BookRepository bRepo = new BookRepository(_dal);
			
			Book book = bRepo.Get(id);
			if(book == null)
				throw new HttpException(404, "Ressource not found");
			
			if(String.IsNullOrEmpty(book.image))
				throw new HttpException(404, "Book doesn't contain an image");
			
			try
			{
				long ret = bRepo.SetImage(id, null);
				if(ret == 0)
					throw new Exception("Book was not updated.");
			}
			catch(Exception ex)
			{
				log.Error(ex);
				throw;
			}
		}

		public void DeleteBook(long id)
		{
			BookRepository bRepo = new BookRepository(_dal);

			Book book = bRepo.Get(id);
			if (book == null)
				throw new HttpException(404, "Ressource not found");

			try
			{
				_dal.BeginTransaction();

				long ret = bRepo.Delete(id);
				if (ret == 0)
					throw new Exception("No book was deleted.");
				
				_dal.CommitTransaction();
			}
			catch (Exception ex)
			{
				_dal.RollbackTransaction();
				log.Error(ex);
				throw;
			}
		}
	}
}
