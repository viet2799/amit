using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Core.Entities;
using MISA.AMIS.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.WebApp.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : BaseEntityController<Employee>
    {
        IEmployeeService _employeeService;
        public EmployeeController(IEmployeeService employeeService) : base(employeeService)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Phân trang dữ liệu
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        /// CreatedBy:ntquan(11/05/2021)
        [HttpGet("paging")]
        public IActionResult GetPaging(int pageIndex, int pageSize)
        {
            var entities = _employeeService.GetPaging(pageIndex, pageSize);
            return Ok(entities);
        }

        /// <summary>
        /// Lấy mã nhân viên mới
        /// </summary>
        /// <returns>Mã nhân viên mới</returns>
        /// CreatedBy:ntquan(11/05/2021)
        [HttpGet("NewEmployeeCode")]
        public IActionResult GetNewEmployeeCode()
        {
            var entities = _employeeService.GetNewEmployeeCode();
            return Ok(entities);
        }

        /// <summary>
        /// Kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">mã NV</param>
        /// <returns>true nếu đã tồn tại, false nếu chưa tồn tại</returns>
        /// CreatedBy: ntquan(11/05/2021)
        [HttpGet("CheckEmployeeCodeExist")]
        public IActionResult CheckEmployeeCodeExist(string employeeCode)
        {
            var isExist = _employeeService.CheckEmployeeCodeExits(employeeCode);
            return Ok(isExist);
        }

        /// <summary>
        /// Lọc nhân viên
        /// </summary>
        /// <param name="pageIndex">Trang</param>
        /// <param name="pageSize">Số bản ghi / trang</param>
        /// <param name="employeeFilter">keyword</param>
        /// <returns></returns>
        /// CreatedBy: ntquan(13/05/2021)
        [HttpGet("employeeFilter")]
        public IActionResult GetEmployeeFilter(int pageIndex,int pageSize,string employeeFilter)
        {
            var entities = _employeeService.GetEmployeeFilter(pageIndex, pageSize, employeeFilter);
            return Ok(entities);
        }

        /// <summary>
        /// Xoá 
        /// </summary>
        /// <param name="id"> List Id thực thể</param>
        /// <returns>Số bản ghi đã xoá</returns>
        /// CreatedBy: ntquan(11/05/2021)
        [HttpDelete("/multipleEmployee")]
        public IActionResult DeleteMultiple(string id)
        {
            var entity = _employeeService.DeleteMultipleEmployee(id);
            return Ok(entity);
        }
    }
}
