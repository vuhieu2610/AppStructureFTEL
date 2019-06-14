using AppOutSideAPI.Data.Class;
using AppOutSideAPI.Data.Interfaces;
using EntityData;
using EntityData.Common;
using System.Collections.Generic;

namespace AppOutSideAPI.Business
{
    public class UserBusiness
    {
        private IUnitOfWork _uok = null;

        private IUnitOfWork unitOfWork
        {
            get
            {
                if (_uok == null)
                {
                    _uok = new UnitOfWork();
                }

                return _uok;
            }
        }


        public ReturnResult<Users> GetPaging(BaseCondition condition)
        {
            return unitOfWork.UserRepository.GetPaging(condition);
        }

        public ReturnResult<Users> GetSingle(Users item)
        {
            return unitOfWork.UserRepository.GetSingle(item);
        }

        public ReturnResult<Users> InsertSingle(Users item)
        {
            return unitOfWork.UserRepository.Insert(item);
        }

        public ReturnResult<Users> InsertList(List<Users> items)
        {
            return unitOfWork.UserRepository.Insert(items);
        }

        public ReturnResult<Users> UpdateSingle(Users item)
        {
            return unitOfWork.UserRepository.Update(item);
        }

        public ReturnResult<Users> Delete(Users item)
        {
            return unitOfWork.UserRepository.Delete(item);
        }
    }
}
