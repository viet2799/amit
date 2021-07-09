using MISA.AMIS.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Core.Entities
{
    // Dùng để check bắt buộc nhập
    [AttributeUsage(AttributeTargets.Property)]
    public class Required : Attribute
    {
        public string UserMsg = string.Empty;

        public Required(string userMsg = "")
        {
            UserMsg = userMsg;
        }
    }

    // Dùng để check trùng dữ liệu
    [AttributeUsage(AttributeTargets.Property)]
    public class IsDuplicate : Attribute
    {

    }

    //Kiểm tra định dạng Email
    [AttributeUsage(AttributeTargets.Property)]
    public class IsNotEmail : Attribute
    {

    }

    // Lấy tên bảng
    [AttributeUsage(AttributeTargets.Property)]
    public class DisplayName : Attribute
    {
        public string Name { get; set; }
        public DisplayName(string name = null)
        {
            this.Name = name;
        }
    }

    // Lấy độ dài Property
    [AttributeUsage(AttributeTargets.Property)]
    public class MaxLength : Attribute
    {
        #region DECLARE
        public int Length = 0;
        public string UserMsg = string.Empty;
        #endregion


        public MaxLength(int maxLength = 0, string userMsg = "")
        {
            Length = maxLength;
            UserMsg = userMsg;
        }



        public string ErrorMaxLength
        {
            get
            {
                if (Length != 0)
                {
                    return UserMsg;
                }
                return null;
            }
        }
    }

    // Khóa 
    [AttributeUsage(AttributeTargets.Property)]
    public class Primary : Attribute
    {
    }

    // Khóa chính
    [AttributeUsage(AttributeTargets.Property)]
    public class PrimaryKey : Attribute
    {
    }


    public abstract class BaseEntity
    {
        /// <summary>
        /// Xác định trạng thái thêm hay cập nhật
        /// </summary>
        public EntityState EntityState { get; set; } = EntityState.AddNew;

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
