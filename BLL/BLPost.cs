using BLL.Base;
using Common.Utilities;
using DAL.dbmodel;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.ViewModels.Post;
using Model.ViewModels.Project;
using Repository.EF.Repositories;
using Repository.EF.UnitOfWork;

namespace BLL
{
    public class BLPost : BLBase<Post, int>
    {
        private readonly PostRepository _postRepository;
        private readonly PostCategoryRepository _categoryRepository;
        public BLPost(IUnitOfWork unitOfWork, FileUtility fileUtility) : base(unitOfWork, fileUtility)
        {
            _postRepository = _unitOfWork.GetInheritedRepository<PostRepository, Post, int>();
            _categoryRepository = _unitOfWork.GetInheritedRepository<PostCategoryRepository, PostCategory, int>();
        }


        public PostCollectionViewModel GetAllPosts()
        {
            var posts = _repository.GetAll();
            List<PostViewModel> postList = posts.Select(p => new PostViewModel()
            {
                Summary = p.Summary,
                Active = p.Active,
                PostCategoryId = p.PostCategoryId,
                PostCategoryName = _categoryRepository.GetById((int)p.PostCategoryId).Name.ToString(),
                PostId = p.PostId,
                Description = p.Description,
                ImagePath = p.ImagePath,
                RegDate = p.RegDate,
                Title = p.Title,
            }).ToList();

            return new PostCollectionViewModel { Posts = postList };
        }

        public PostCollectionViewModel GetAllPosts(int pageNumber, int pageSize)
        {
            var posts = _repository.GetAll().OrderByDescending(d => d.RegDate).ThenBy(i => i.PostId).Skip((pageNumber - 1) * pageSize)
                            .Take(pageSize)
                            .ToList(); 
            List<PostViewModel> postList = posts.Select(p => new PostViewModel()
            {
                Summary = p.Summary,
                Active = p.Active,
                PostCategoryId = p.PostCategoryId,
                PostCategoryName = _categoryRepository.GetById((int)p.PostCategoryId).Name.ToString(),
                PostId = p.PostId,
                Description = p.Description,
                ImagePath = p.ImagePath,
                RegDate = p.RegDate,
                Title = p.Title,
            }).ToList();

            return new PostCollectionViewModel { Posts = postList };
        }
        public PostCollectionViewModel GetPostsByCategoryId(int categoryId)
        {
            var posts = _postRepository.GetPostsByCategoryID(categoryId);

            List<PostViewModel> postList = posts.Select(p => new PostViewModel()
            {
                Summary = p.Summary,
                Active = p.Active,
                PostCategoryId = p.PostCategoryId,
                PostCategoryName = _categoryRepository.GetById((int)p.PostCategoryId).Name.ToString(),
                PostId = p.PostId,
                Description = p.Description,
                ImagePath = p.ImagePath,
                RegDate = p.RegDate,
                Title = p.Title,
            }).ToList();

            return new PostCollectionViewModel { Posts = postList };

        }


        public PostViewModel GetPostById(int postId)
        {
            var post = _repository.GetById(postId);
            return new PostViewModel()
            {
                Summary = post.Summary,
                Active = post.Active,
                Description = post.Description,
                ImagePath = post.ImagePath,
                PostCategoryId = post.PostCategoryId,
                PostCategoryName = _categoryRepository.GetById((int)post.PostCategoryId).Name.ToString(),
                PostId = post.PostId,
                RegDate = post.RegDate,
                Title = post.Title,
                Categories = _categoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Value = c.PostCategoryId.ToString(),
                    Text = c.Name,
                    Selected = c.PostCategoryId == post.PostCategoryId
                }).ToList(),
            };
        }

        public int AddNewPost(PostViewModel postViewModel)
        {
            if (postViewModel == null)
            {
                return -1;
            }
            else
            {
                try
                {
                    var newPost = new Post()
                    {
                        Summary = postViewModel.Summary,
                        Active = postViewModel.Active,
                        Description = postViewModel.Description,
                        RegDate = DateTime.UtcNow,
                        PostCategoryId = postViewModel.PostCategoryId,
                        Title = postViewModel.Title,
                    };
                    _repository.Add(newPost);
                    _unitOfWork.Complete();

                    var lastId = _repository.GetAll().Last().PostId;
                    var subDirectory = "posts/" + lastId.ToString() + "/";
                    if (postViewModel.ImageFile != null)
                    {
                        var path = _fileUtility.UploadFileAsync(postViewModel.ImageFile, subDirectory, null);
                        var post = _repository.GetById(lastId);
                        if (post != null)
                        {
                            post.ImagePath = path.Result;
                            _repository.Update(post);
                            _unitOfWork.Complete();
                        }
                    };

                    return lastId;
                }
                catch
                {
                    return -1;
                }
            }


        }


        public int UpdatePost(PostViewModel postViewModel)
        {
            if (postViewModel == null)
            {
                return -1;
            }
            else
            {
                try
                {
                    var post = _repository.GetById(postViewModel.PostId);

                    post.Summary = postViewModel.Summary;
                    post.Active = postViewModel.Active;
                    post.Description = postViewModel.Description;
                    post.RegDate = DateTime.UtcNow;
                    post.PostCategoryId = postViewModel.PostCategoryId;
                    post.Title = postViewModel.Title;

                    if (postViewModel.ImageFile != null)
                    {
                        var subDirectory = "posts/" + postViewModel.PostId.ToString() + "/";
                        var path = _fileUtility.UploadFileAsync(postViewModel.ImageFile, subDirectory, null);
                        _fileUtility.DeleteFile(post.ImagePath);
                        post.ImagePath = path.Result;
                    };

                    _repository.Update(post);
                    _unitOfWork.Complete();
                    return postViewModel.PostId;
                }
                catch
                {
                    return -1;
                }
            }
        }


        public int RemovePost(int postId)
        {
            var post = _repository.GetById(postId);
            if (post != null)
            {

                _fileUtility.DeleteDirectory(post.ImagePath);

                _repository.Remove(post);
                _unitOfWork.Complete();

                return postId;
            }
            else
            {
                return -1;
            }

        }


        public PostViewModel SelectListCatgeories()
        {
            var postViewModel = new PostViewModel()
            {
                Categories = _categoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.PostCategoryId.ToString(),
                    
                }).ToList(),
            };

            return postViewModel;
        }

        #region Post Categories

        public List<PostCategoryViewModel> GetCategories()
        {
            var categories= _categoryRepository.GetAll();

            List<PostCategoryViewModel> categoryList = categories.Select(c => new PostCategoryViewModel()
            {
                Active = c.Active,
                Name = c.Name,
                PostCategoryId = c.PostCategoryId,
                Posts = c.Posts.Select(p => new PostViewModel()
                {
                    Summary = p.Summary,
                    Active = p.Active,
                    PostCategoryId = p.PostCategoryId,
                    Description = p.Description,
                    ImagePath = p.ImagePath,
                    PostId = p.PostId,
                    RegDate = p.RegDate,
                    Title = p.Title,
                }).ToList(),
            }).ToList();

            return categoryList;
        }


        public PostCollectionViewModel CategoriesByCount()
        {
            var posts = _repository.GetAll();
            List<string> cats = _categoryRepository.GetAll().Select(c => c.Name + "/" + posts.Where(i=> i.PostCategoryId == c.PostCategoryId).Count().ToString()).ToList();

            return new PostCollectionViewModel { CategoriesByCount = cats };
        }

        #endregion

    }
}
