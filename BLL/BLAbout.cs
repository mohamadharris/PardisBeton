using BLL.Base;
using Common.Utilities;
using DAL.dbmodel;
using Model.ViewModels.About;
using Repository.EF.Repositories;
using Repository.EF.UnitOfWork;

namespace BLL
{
    public class BLAbout : BLBase<About, int>
    {
        private readonly PersonnelRepository _personnelRepository;
        public BLAbout(IUnitOfWork unitOfWork, FileUtility fileUtility) : base(unitOfWork, fileUtility)
        {
            _personnelRepository = _unitOfWork.GetInheritedRepository<PersonnelRepository, Personnel, int>();
        }


        #region About
        public AboutViewModel GetAboutSummary(int id = 1)
        {
            var about = _repository.GetById(id);
            return new AboutViewModel
            {
                Summary = about.Summary,
                SummaryImagePath = about.SummaryImagePath,
                AboutId = id
            };
        }

        public AboutViewModel GetAboutContent(int id = 1)
        {
            var about = _repository.GetById(id);
            return new AboutViewModel
            {
                PageContent = about.PageContent,
                AboutId = id
            };
        }


        public int UpdateAbout(AboutViewModel aboutViewModel)
        {
            if (aboutViewModel == null)
            {
                return -1;
            }
            else
            {
                var about = _repository.GetById(aboutViewModel.AboutId);
                if (aboutViewModel.Summary != null)
                {
                    about.Summary = aboutViewModel.Summary;
                    if (aboutViewModel.ImageFile != null)
                    {
                        var subDirectory = "about/";
                        var path = _fileUtility.UploadFileAsync(aboutViewModel.ImageFile, subDirectory, null);
                        about.SummaryImagePath = path.Result;
                    }


                }
                else if (aboutViewModel.PageContent != null)
                {
                    about.PageContent = aboutViewModel.PageContent;
                   
                }
                _repository.Update(about);
                _unitOfWork.Complete();

                return aboutViewModel.AboutId;

            }

        }
        #endregion

        #region Personnel

        public AboutCollectionViewModel GetPersonnels()
        {
            var personnels = _personnelRepository.GetAll();
            var personnelList = personnels.Select(p => new PersonnelViewModel()
            {
                Sex = p.Sex,
                Description = p.Description,
                Family = p.Family,
                Name = p.Name,
                ImagePath = p.ImagePath,
                Nickname = p.Nickname,
                PersonnelId = p.PersonnelId,
                Position = p.Position,

            }).AsEnumerable();

            return new AboutCollectionViewModel() { Personnels = personnelList };
        }


        public PersonnelViewModel GetPersonnelById(int id)
        {
            var personnel = _personnelRepository.GetById(id);
            if (personnel != null)
            {
                var personnelViewModel = new PersonnelViewModel()
                {
                    Sex = personnel.Sex,
                    Description = personnel.Description,
                    Family = personnel.Family,
                    Name = personnel.Name,
                    ImagePath = personnel.ImagePath,
                    Nickname = personnel.Nickname,
                    PersonnelId = personnel.PersonnelId,
                    Position = personnel.Position

                };

                return personnelViewModel;
            }
            else
            {
                return null;
            }
        }


        public int AddNewPersonnel(PersonnelViewModel personnelViewModel)
        {
            if (personnelViewModel == null)
            {
                return -1;
            }
            else
            {
                var newPersonnel = new Personnel()
                {
                    Sex = personnelViewModel.Sex,
                    Description = personnelViewModel.Description,
                    Family = personnelViewModel.Family,
                    Name = personnelViewModel.Name,
                    Nickname = personnelViewModel.Nickname,
                    Position = personnelViewModel.Position
                };
                _personnelRepository.Add(newPersonnel);
                _unitOfWork.Complete();

                var lastId = _personnelRepository.GetAll().Last().PersonnelId;
                var subDirectory = "about/personnel/";
                if (personnelViewModel.ImageFile != null)
                {
                    var path = _fileUtility.UploadFileAsync(personnelViewModel.ImageFile, subDirectory, null);
                    var personnel = _personnelRepository.GetById(lastId);
                    if (personnel != null)
                    {
                        personnel.ImagePath = path.Result;
                        _personnelRepository.Update(personnel);
                        _unitOfWork.Complete();
                    }
                }

                return lastId;


            }
        }


        public int UpdatePersonnel(PersonnelViewModel personnelViewModel)
        {
            if (personnelViewModel == null)
            {
                return -1;
            }
            else
            {
                var personnel = _personnelRepository.GetById(personnelViewModel.PersonnelId);
                if (personnel != null)
                {
                    personnel.Sex = personnelViewModel.Sex;
                    personnel.Description = personnelViewModel.Description;
                    personnel.Family = personnelViewModel.Family;
                    personnel.Name = personnelViewModel.Name;
                    personnel.Nickname = personnelViewModel.Nickname;
                    personnel.Position = personnelViewModel.Position;

                    var subDirectory = "about/personnel/";
                    if (personnelViewModel.ImageFile != null)
                    {
                        var path = _fileUtility.UploadFileAsync(personnelViewModel.ImageFile, subDirectory, null);
                        if ( personnel.ImagePath != null)
                        {
                            try
                            {
                                _fileUtility.DeleteFile(personnel.ImagePath);
                            }
                            catch 
                            {
                                
                            }
                            
                        }
                        personnel.ImagePath = path.Result;
                    }
                    _personnelRepository.Update(personnel);
                    _unitOfWork.Complete();
                    return personnel.PersonnelId;
                }
                else
                {
                    return -1;
                }
            }
        }

        public int RemovePersonnel(int personnelId)
        {
            var personnel = _personnelRepository.GetById(personnelId);
            if (personnel != null)
            {

                _fileUtility.DeleteDirectory(personnel.ImagePath);

                _personnelRepository.Remove(personnel);
                _unitOfWork.Complete();

                return personnelId;
            }
            else
            {
                return -1;
            }

        }
        #endregion

    }
}
