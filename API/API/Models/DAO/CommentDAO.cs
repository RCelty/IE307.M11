using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using API.Models.DTO;
using API.Models.EF;

namespace API.Models.DAO
{
    public class CommentDAO
    {
        private static CommentDAO instance;

        public static CommentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CommentDAO();
                }
                return instance;
            }
            private set => instance = value;
        }

        PhoneStoreEntities1 db = new PhoneStoreEntities1();

        public async Task<List<CommentDTO>> GetAllComment()
        {
            var resultList = (await db.Comments
                        .ToListAsync())
                        .Select(c => new CommentDTO(c))
                        .ToList();

            return resultList;
        }

        public async Task<int> AddComment(CommentDTO commentDTO)
        {
            var comment = new Comment()
            {
                ProductID = commentDTO.ProductID,
                CustomerID = commentDTO.CustomerID,
                CreationDate = DateTime.Now,
                Content = commentDTO.Content,
                Rating = commentDTO.Rating
            };

            Customer customer = db.Customers.SingleOrDefault(c => c.ID == comment.CustomerID);
            comment.Customer = customer;

            //Product product = db.Products.SingleOrDefault(p => p.ID == comment.ProductID);
            //db.Entry(product).State = EntityState.Modified;
            //product.Comments.Add(comment);            

            db.Entry(comment).State = EntityState.Added;

            try
            {
                db.Comments.Add(comment);
                await db.SaveChangesAsync();
                return comment.ID;
            }
            catch (Exception e)
            {
                return -1;
                throw e;
            }
        }

        public async Task<bool> UpdateComment(CommentDTO commentDTO)
        {
            var result = db.Comments.SingleOrDefault(c => c.ID == commentDTO.ID);

            db.Entry(result).State = EntityState.Modified;

            try
            {
                result.Content = commentDTO.Content;
                result.Rating = commentDTO.Rating;
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<bool> DeleteComment(int ID)
        {
            var result = db.Comments.SingleOrDefault(c => c.ID == ID);

            db.Entry(result).State = EntityState.Deleted;

            try
            {
                db.Comments.Remove(result);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
                throw e;
            }
        }

        public async Task<List<CommentDTO>> GetCommentByProductID(int ID)
        {
            var resultList = (await db.Comments
                .ToListAsync())
                .Select(c => new CommentDTO(c))
                .ToList();
            resultList = resultList.FindAll(c => c.ProductID == ID);
            return resultList;
        }
    }
}
