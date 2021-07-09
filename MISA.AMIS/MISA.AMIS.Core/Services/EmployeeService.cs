using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Exceptions;
using MISA.AMIS.Core.Interfaces.Repository;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Services
{
    public class EmployeeService : BaseService<Employee>, IEmployeeService
    {
        IEmployeeRepository _employeeReponsitory;
        ServiceResult _serviceResult;

        public EmployeeService(IEmployeeRepository employeeRepository) : base(employeeRepository)
        {
            _employeeReponsitory = employeeRepository;
             _serviceResult = new ServiceResult();
        }

        public bool CheckEmployeeCodeExits(string employeeCode)
        {
            var isExist = _employeeReponsitory.CheckEmployeeCodeExits(employeeCode);
            return isExist;
        }

        public ServiceResult DeleteMultipleEmployee(string listId)
        {
            _serviceResult.data = _employeeReponsitory.DeleteMultipleEmployee(listId);
            return _serviceResult;
        }

        public int GetCountEmployee()
        {
            var count = _employeeReponsitory.GetCountEmployee();
            return count;
        }

        public ServiceResult GetEmployeeFilter(int pageIndex, int pageSize, string employeeFilter)
        {
            var count = _employeeReponsitory.GetCountEmployee();
            var entities = _employeeReponsitory.GetEmployeeFilter(pageIndex, pageSize,employeeFilter);
            _serviceResult.data = entities;
            _serviceResult.Total = count;
            return _serviceResult;
        }

        public string GetNewEmployeeCode()
        {
            var code = _employeeReponsitory.GetMaxEmployeeCode();
            string letters = string.Empty;
            string numbers = string.Empty;

            foreach (char c in code)
            {
                if (Char.IsLetter(c))
                {
                    letters += c;
                }
                if (Char.IsNumber(c))
                {
                    numbers += c;
                }
            }
            var newCode = $"NV-{Int32.Parse(numbers) + 1}";
            return newCode;
        }

        public ServiceResult GetPaging(int pageIndex, int pageSize)
        {
            var count = _employeeReponsitory.GetCountEmployee();
            var entities = _employeeReponsitory.GetPaging(pageIndex, pageSize);
            _serviceResult.data = entities;
            _serviceResult.Total = count;
            return _serviceResult;
        }

        protected override void ValidateCustom(Employee employee)
        {
            //Check phòng ban có tồn tại hay không
            if (!string.IsNullOrEmpty(employee.DepartmentId.ToString()))
            {
                var res = _employeeReponsitory.CheckDepartmentExits(employee.DepartmentId);
                if (!res)
                {
                    throw new ValidateExceptions("Thông tin phòng ban không tồn tại trong hệ thống");
                }
            }

        }
    }
}
