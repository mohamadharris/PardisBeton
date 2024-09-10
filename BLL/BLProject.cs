using BLL.Base;
using Common.Interfaces;
using Common.Utilities;
using DAL.dbmodel;
using Microsoft.AspNetCore.Http;
using Model.ViewModels.Project;
using Repository.EF.Repositories;
using Repository.EF.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class BLProject : BLBase<Project, int>, ITransientService
    {
        private readonly ProjectsRepository _projectRepository;
        public BLProject(IUnitOfWork unitOfWork, FileUtility fileService) : base(unitOfWork, fileService)
        {
            _projectRepository = _unitOfWork.GetInheritedRepository<ProjectsRepository,Project,int>();
        }


        public ProjectViewModel GetProjectById(int id)
        {
            var project = _repository.GetById(id);
            return new ProjectViewModel
            {
                Active = project.Active,
                ShowAtHome = project.ShowAtHome,
                Client = project.Client,
                Description = project.Description,
                ImagePath = project.ImagePath,
                Location = project.Location,
                Name = project.Name,
                Priority = project.Priority,
                ProjectId = id,
                RegDate = project.RegDate,
                Summary = project.Summary
            };
        }


        public ProjectCollectionViewModel GetAllProjects()
        {
            var projects = _repository.GetAll();
            var projectCollectionViewModel = projects.Select(p => new ProjectViewModel()
            {
                Active = p.Active,
                ShowAtHome = p.ShowAtHome,
                Client = p.Client,
                Description = p.Description,
                ImagePath = p.ImagePath,
                Location = p.Location,
                Name = p.Name,
                Priority = p.Priority,
                ProjectId = p.ProjectId,
                RegDate = p.RegDate,
                Summary = p.Summary,
            }).AsEnumerable();
            return new ProjectCollectionViewModel() { Projects = projectCollectionViewModel };

        }


        public ProjectCollectionViewModel GetAllProjects(int skip, int take)
        {
            var projects = _repository.GetAll();
            var projectCollectionViewModel = projects.Select(p => new ProjectViewModel()
            {
                Active = p.Active,
                ShowAtHome = p.ShowAtHome,
                Client = p.Client,
                Description = p.Description,
                ImagePath = p.ImagePath,
                Location = p.Location,
                Name = p.Name,
                Priority = p.Priority,
                ProjectId = p.ProjectId,
                RegDate = p.RegDate,
                Summary = p.Summary,
            }).AsEnumerable().Skip(skip).Take(take);
            return new ProjectCollectionViewModel() { Projects = projectCollectionViewModel };

        }


        public ProjectCollectionViewModel GetRandomProjects(int count)
        {
            var rnd = new Random();
            var projects = _projectRepository.GetAll().OrderBy(p => rnd.Next()).Take((int)count);
            var projectCollectionViewModel = projects.Select(p => new ProjectViewModel()
            {
                Active = p.Active,
                ShowAtHome = p.ShowAtHome,
                Client = p.Client,
                Description = p.Description,
                ImagePath = p.ImagePath,
                Location = p.Location,
                Name = p.Name,
                Priority = p.Priority,
                ProjectId = p.ProjectId,
                RegDate = p.RegDate,
                Summary = p.Summary,
            });
            return new ProjectCollectionViewModel() { RandomProjects = projectCollectionViewModel };
        }


        public int AddNewProject(ProjectViewModel projectViewModel)
        {

            if (projectViewModel == null)
            {
                return -1;
            }
            else
            {
                try
                {
                    var newProject = new Project()
                    {
                        ShowAtHome = projectViewModel.ShowAtHome,
                        Client = projectViewModel.Client,
                        Description = projectViewModel.Description,
                        ImagePath = projectViewModel.ImagePath,
                        Location = projectViewModel.Location,
                        Name = projectViewModel.Name,
                        Priority = projectViewModel.Priority,
                        RegDate = projectViewModel.RegDate,
                        Summary = projectViewModel.Summary,
                        Active = projectViewModel.Active,

                    };

                    _repository.Add(newProject);
                    _unitOfWork.Complete();

                    var lastId = _repository.GetAll().Last().ProjectId;
                    var subDirectory = "projects/" + lastId.ToString() + "/";
                    if (projectViewModel.ImageFile != null)
                    {
                        var path = _fileUtility.UploadFileAsync(projectViewModel.ImageFile, subDirectory, null);
                        var project = _repository.GetById(lastId);
                        if (project != null)
                        {
                            project.ImagePath = path.Result;
                            _repository.Update(project);
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


        public int UpdateProject(ProjectViewModel projectViewModel)
        {

            if (projectViewModel == null)
            {
                return -1;
            }
            else
            {
                try
                {
                    var project = _repository.GetById(projectViewModel.ProjectId);
                    if (project != null)
                    {
                        project.ShowAtHome = projectViewModel.ShowAtHome;
                        project.Client = projectViewModel.Client;
                        project.Description = projectViewModel.Description;
                        project.Location = projectViewModel.Location;
                        project.Name = projectViewModel.Name;
                        project.Priority = projectViewModel.Priority;
                        project.RegDate = projectViewModel.RegDate;
                        project.Summary = projectViewModel.Summary;
                        project.Active = projectViewModel.Active;

                        var subDirectory = "projects/" + projectViewModel.ProjectId.ToString() + "/";
                        if (projectViewModel.ImageFile != null)
                        {
                            var path = _fileUtility.UploadFileAsync(projectViewModel.ImageFile, subDirectory, null);

                            if (project.ImagePath != null)
                            {

                                try
                                {
                                    _fileUtility.DeleteFile(projectViewModel.ImagePath);
                                }
                                catch
                                {
                                }
                                
                            }
                            project.ImagePath = path.Result;
                        }
                        _repository.Update(project);
                        _unitOfWork.Complete();



                    };

                    return projectViewModel.ProjectId;
                }
                catch
                {

                    return -1;
                }

            }
        }



        public int RemoveProject(int projectId)
        {
            var project = _repository.GetById(projectId);
            if (project != null)
            {

                _fileUtility.DeleteDirectory(project.ImagePath);

                _repository.Remove(project);
                _unitOfWork.Complete();

                return projectId;
            }
            else
            {
                return -1;
            }

        }



   
    }
}
