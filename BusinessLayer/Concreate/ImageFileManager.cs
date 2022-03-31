using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concreate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concreate
{
    public class ImageFileManager : IImageFileService
    {
        IImageFileDal _imagefileDal;

        public ImageFileManager(IImageFileDal imagefileDal)
        {
            _imagefileDal = imagefileDal;
        }

        public ImageFile GetByIdImageFile(int id)
        {
            return _imagefileDal.Get(x => x.ImageID == id);
        }

        public List<ImageFile> GetList()
        {
            return _imagefileDal.List();
        }

        public void ImageAdd(ImageFile imageFile)
        {
            _imagefileDal.Insert(imageFile);
        }

        public void ImageDelete(ImageFile imageFile)
        {
            _imagefileDal.Delete(imageFile);
        }

        public void ImageUpdate(ImageFile imageFile)
        {
            _imagefileDal.Update(imageFile);
        }
    }
}