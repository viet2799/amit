<template>
    <div class="content">
        <BaseLoading :showLoading="isLoading" />
        <!-- header content -->
        <div class="content__header flex items-center">
            <p class="content__header--title">Nhân viên</p>
            <button class="m-btn" id="btnAdd" @click="onAddEmployee">
                Thêm mới nhân viên
            </button>
        </div>

        <!-- filter -->
        <div class="grid-filter flex items-center sticky-left">
            <div class="search flex items-center">
                <input type="text"
                       class="m-input"
                       placeholder="Tìm theo mã, tên nhân viên"
                       @input="onSearch($event.target.value)" />
                <div class="icon-search mi-16"></div>
            </div>
            <button class="reload mi-24" @click="onLoadEmployee"></button>
        </div>

        <div class="content__grid">
            <!-- <div class="content__grid--data"> -->
            <!-- grid -->
            <table border="0" cellspacing="0" width="100%" id="tblEmployees">
                <thead>
                <th class="td-left-16"></th>
                <th class="sticky-left left-16 justify-center">
                    <input type="checkbox"
                           name=""
                           id=""
                           class="right-5 m-b-5"
                           @click="selectAll"
                           ref="selectAll" />
                </th>
                <th class="sticky-left left-66">Mã nhân viên</th>
                <th>Tên nhân viên</th>
                <th>Giới tính</th>
                <th class="text-center">Ngày sinh</th>
                <th>Số CMND</th>
                <th>Chức danh</th>
                <th>Tên đơn vị</th>
                <th>Số tài khoản</th>
                <th>Tên ngân hàng</th>
                <th>Chi nhánh tk ngân hàng</th>
                <th class="sticky-right text-center">Chức năng</th>
                <th class="td-white-30"></th>
                <th class="td-grey-30"></th>
                </thead>
                <tbody>
                    <tr v-for="(item, index) in dataEmployee"
                        :key="index"
                        @dblclick="rowdblClick(item)">
                        <td class="td-left-16"></td>
                        <!-- checkbox -->
                        <td class="sticky-left left-16 justify-center"
                            :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            <input type="checkbox"
                                   name=""
                                   id=""
                                   class="right-5"
                                   @click="highlight(item.EmployeeId, $event)"
                                   :checked="selectedUser.indexOf(item.EmployeeId) != -1" />
                        </td>

                        <!-- Mã NV -->
                        <td :title="item.EmployeeCode"
                            class="sticky-left left-66"
                            :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.EmployeeCode }}
                        </td>

                        <!-- Tên NV -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.EmployeeName }}
                        </td>

                        <!-- Giới tính -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.GenderName }}
                        </td>

                        <!-- Ngày sinh -->
                        <td class="text-center"
                            :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ $fn.fnFormatDate(item.DateOfBirth) }}
                        </td>

                        <!-- Số CMND -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.IdentityNumber }}
                        </td>

                        <!-- Chức danh -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.EmployeePosition }}
                        </td>

                        <!-- Tên đơn vị -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
            }"></td>

                        <!-- Số tài khoản -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
            }">
                            {{ item.BankAccountNumber }}
                        </td>

                        <!-- Tên ngân hàng -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.BankName }}
                        </td>

                        <!-- Chi nhánh tài khoản ngân hàng -->
                        <td :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            {{ item.BankProvinceName }}
                        </td>

                        <!--Chức năng -->
                        <td class="sticky-right flex text-center items-center justify-center"
                            :class="{
                selected: selectedUser.indexOf(item.EmployeeId) != -1,
              }">
                            <button class="btn-edit"
                                    @click="rowdblClick(item)"
                                    :class="{
                  selected: selectedUser.indexOf(item.EmployeeId) != -1,
                }">
                                Sửa
                            </button>
                            <div class="button-arrow flex justify-center"
                                 @click="openContextMenu($event, item)">
                                <div class="icon-arrow-up"></div>
                            </div>
                        </td>
                        <td class="td-white-30"></td>
                        <td class="td-grey-30"></td>
                    </tr>
                </tbody>
            </table>
        </div>

        <div class="no-data" v-if="isNoData">
            <img src="https://actappg2.misacdn.net/img/bg_report_nodata.76e50bd8.svg"
                 alt=""
                 width="132px"
                 height="74px" />
            <p>Không có dữ liệu</p>
        </div>
        <Pagination :total-pages="Math.round(total / pageSize)"
                    :total="total"
                    :per-page="10"
                    :current-page="currentPage"
                    v-if="!isNoData"
                    @pagechanged="onPageChange" />

        <Alert :isShow="isShowAlert" :message="messageAlert" :cls="iconCls">
            <div class="flex w-full justify-between">
                <button class="m-btn-second" @click="onCloseAlert">Không</button>
                <button class="m-btn" @click="onDeleteEmployee">Có</button>
            </div>
        </Alert>

        <Detail :department="department"
                :title="title"
                @onLoad="onLoadEmployee"
                ref="detail" />

        <context-menu :display="showContextMenu" ref="menu">
            <ul>
                <li>Nhân bản</li>
                <li @click="openConfirmDelete">Xoá</li>
                <li>Ngưng sử dụng</li>
            </ul>
        </context-menu>
    </div>
</template>

<script>
    import axios from "axios";
    import Pagination from "../../components/pagination/Pagination";
    import Detail from "./EmployeeDetail";
    import BaseLoading from "../../components/base/BaseLoading";
    import ContextMenu from "../../components/base/ContextMenu";
    import Alert from "../../components/Alert/Alert";

    export default {
        components: {
            Detail,
            BaseLoading,
            ContextMenu,
            Alert,
            Pagination,
        },

        computed: {
            sortedEmployees: function () {
                return [...this.dataEmployee].sort((a, b) => {
                    let modifier = 1;
                    if (this.currentSortDir === "desc") modifier = -1;
                    if (a[this.currentSort] < b[this.currentSort]) return -1 * modifier;
                    if (a[this.currentSort] > b[this.currentSort]) return 1 * modifier;
                    return 0;
                });
            },

            pageSize() {
                return this.$store.getters.getPageSize;
            },
        },
        data() {
            return {
                API_HOST: this.$Const.API_HOST,
                isLoading: false,
                showContextMenu: false,
                isShowAlert: false,
                isNoData: false,
                messageAlert: "",
                iconCls: "",
                dataEmployee: [],
                selectedUser: [],
                dataContext: {},
                dataRow: {},
                department: [],
                title: "",
                currentPage: 1,
                total: 0,
                isDeleteByShortcut:false,
                timeout:null
            };
        },
        methods: {
            /**
             * Chuyển sang trang khác
             * @param page trang đich muốn tới
             * CreatedBy: ntquan(16/05/2021)
             */
            onPageChange(page) {
                var me = this;
                //var pageSize= size||10;
                var pageIndex = page || 1;
                me.currentPage = page;
                me.isLoading = true;
                var url = `${me.API_HOST}/api/v1/Employees/paging?pageIndex=${pageIndex}&pageSize=${me.pageSize}`;
                axios
                    .get(url)
                    .then(async (response) => {
                        me.dataEmployee = response.data.data;
                        me.total = !response.data.Total ? 0 : response.data.Total;
                        me.isLoading = false;
                        console.log(me.dataEmployee);
                        if (me.dataEmployee.length == 0) me.isNoData = true;
                        else me.isNoData = false;
                        me.dataRow = {};
                        me.selectedUser=[]
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            },

            /**
             * Hiện context menu
             * @param e Event
             * @param item dữ liệu nv
             * CreatedBy: ntquan(14/05/2021)
             */
            openContextMenu(e, item) {
                this.dataContext = {};
                this.dataContext = item;
                this.$refs.menu.open(e);
            },

            /**
             * Lấy dữ liệu nhân viên
             * CreatedBy:ntquan(13/05/2021)
             */
            onLoadEmployee() {
                var me = this;
                me.onPageChange(1);
            },

            /**
             * Tìm kiếm NV theo mã, tên hoặc sđt
             * @param value giá trị để tìm kiếm
             * CreatedBy:ntquan(15/05/2021)
             */
            onSearch(value) {
                try {               
                    var me = this;
                    var url = "";
                    if (me.timeout) {  
                        clearTimeout(me.timeout);
                    }
                    me.timeout=setTimeout(function () {
                        if (value == "")
                            url = `${me.API_HOST}/api/v1/Employees/paging?pageIndex=1&pageSize=${me.pageSize}`;
                        else
                            url = `${me.API_HOST}/api/v1/Employees/employeeFilter?pageIndex=1&pageSize=${me.pageSize}&employeeFilter=${value}`;
                        axios
                            .get(url)
                            .then((response) => {
                                me.dataEmployee = response.data.data;
                                me.total = response.data.Total;
                                console.log(me.dataEmployee);
                                if (me.dataEmployee.length == 0) me.isNoData = true;
                                else me.isNoData = false;
                                me.selectedUser=[]
                            })
                            .catch((err) => {
                                console.log(err);
                            });
                    }, 100);
                } catch (error) {
                    console.warn(error);
                }
            },

            /**
             * Lấy mã nhân viên mới
             * CreatedBy:ntquan(13/05/2021)
             */
            async genEmployeeCode() {
                try {                
                    let url = this.API_HOST + "/api/v1/Employees/NewEmployeeCode";
                    var response = await axios.get(url);
                    return response.data;
                } catch (error) {
                    console.warn(error);
                }
            },

            /**
             * Hiển thị thêm mới nhân viên
             * CreatedBy:ntquan(13/05/2021)
             */
            async onAddEmployee() {
                var me = this;
                me.dataRow = {};
                me.dataRow.EmployeeCode = await me.genEmployeeCode();
                me.dataRow.Gender = me.$Const.MALE;
                me.title = "Thêm mới nhân viên";
                me.$store.commit("setDataRow", me.dataRow);
                me.$store.commit("toggleDialog");
                me.$nextTick(function () {
                    me.$refs.detail.$refs.EmployeeCode.focus();
                });
            },

            /**
             * Lấy danh sách phòng ban
             * CreatedBy:ntquan(13/05/2021)
             */
            async getDepartment() {
                var me = this;
                let url = `${me.API_HOST}/api/v1/Departments`;
                const response = await axios.get(url);
                me.department = response.data;
            },

            /**
             * Hiển thị thông tin nhân viên
             * @param item dữ liệu  của hàng click
             * CreatedBy:ntquan(13/05/2021)
             */
            async rowdblClick(item) {
                var me = this;
                var res = await me.onGetEmployeeById(item.EmployeeId);
                if (res.DateOfBirth != null)
                    res.DateOfBirth = me.$fn.fnFormatDateInput(res.DateOfBirth);
                if (res.IdentityDate != null)
                    res.IdentityDate = me.$fn.fnFormatDateInput(item.IdentityDate);
                me.$store.commit("setDataRow", res);
                me.title = "THÔNG TIN NHÂN VIÊN";
                me.$store.commit("toggleDialog");
                me.$nextTick(function () {
                    me.$refs.detail.$refs.EmployeeCode.focus();
                });
            },

            /**
             * Lấy thông tin nhân viên bằng id
             * @param id Id NV
             * CreatedBy: ntquan(15/05/2021)
             */
            async onGetEmployeeById(id) {
                var me = this;
                const url = `${me.API_HOST}/api/v1/Employees/${id}`;
                const response = await axios.get(url);
                return response.data;
            },

            /**
             * Hiển thị alert xác nhận xoá
             * CreatedBy:ntquan(14/05/2021)
             */
            openConfirmDelete() {
                var me = this;
                me.$refs.menu.close();
                me.iconCls = "icon-warning-alert";
                me.messageAlert = `Bạn có thực sự muốn xoá Nhân viên <${me.dataContext.EmployeeCode}> không?`;
                me.isShowAlert = true;
            },

            /**
             * Thực hiện xoá Nhân viên
             * CreatedBy:ntquan(14/05/2021)
             */
            async onDeleteEmployee() {
                var me = this;
                if( me.isDeleteByShortcut) return;
                var url = `${me.API_HOST}/api/v1/Employees/${me.dataContext.EmployeeId}`;
                await axios.delete(url);
                me.isShowAlert = false;
                me.onLoadEmployee();
            },


            /**
             * Phím tắt
             * CreatedBy: ntquan(16/05/2021)
             */
            handleKeyUp(e) {
                var me = this;
                if (!me.$store.getters.getIsShow) {
                    if (e.ctrlKey && e.keyCode == 68) {
                        var countEmployee = me.selectedUser.indexOf('All')!=-1?me.selectedUser.length - 1: me.selectedUser.length
                        e.preventDefault();
                        e.stopPropagation();
                        console.log(countEmployee);
                        me.isDeleteByShortcut =true;
                        me.iconCls = "icon-warning-alert";
                        me.messageAlert = `Bạn có thực sự muốn xoá <${countEmployee}> Nhân viên không?`;
                        me.isShowAlert = true;
                        //console.log(e);
                    }
                }
            },

            /**
             * Thay đổi background row
             * @param id Id nhân viên
             * CreatedBy:ntquan(13/05/2021)
             */
            highlight(id) {
                var me = this;
                var elIndex = me.selectedUser.indexOf(id);
                var countEmployee = me.dataEmployee.length;
                var countSelectedUser =
                    me.selectedUser.indexOf("All") != -1
                        ? me.selectedUser.length - 1
                        : me.selectedUser.length;
                if (elIndex != -1) {
                    me.selectedUser = me.selectedUser.filter(function (e) {
                        return e !== id && e != "All";
                    });
                    this.$refs.selectAll.checked = false;
                } else {
                    me.selectedUser.push(id);
                    if (countSelectedUser + 1 == countEmployee) {
                        me.selectedUser.push("All");
                        this.$refs.selectAll.checked = true;
                    }
                }
            },

            /**
             * Thay đổi background tất cả row
             * CreatedBy:ntquan(13/05/2021)
             */
            selectAll() {
                var me = this;
                if (me.selectedUser.indexOf("All") != -1) {
                    me.selectedUser = [];
                } else {
                    me.selectedUser = [];
                    me.dataEmployee.forEach((element) => {
                        me.selectedUser.push(element.EmployeeId);
                    });
                    me.selectedUser.push("All");
                }
            },

            /**
             * Đóng Alert
             * CreatedBy:ntquan(14/05/2021)
             */
            onCloseAlert() {
                this.isDeleteByShortcut=false;
                this.isShowAlert = false;
                this.messageAlert = "";
                this.iconCls = "";
            },
        },

        created() {
            this.onLoadEmployee();
            this.getDepartment();
            document.addEventListener("keydown", this.handleKeyUp);
        },
        mounted() { },
    };
</script>

<style>
    .content {
        overflow: scroll;
        flex-direction: column;
    }

        .content .content__header {
            justify-content: space-between;
            left: 0;
            width: 100%;
            padding: 22px 0px 16px 0px;
            position: sticky;
            top: 0;
            z-index: 10;
            background-color: #f4f5f6;
        }

            .content .content__header .content__header--title {
                font-size: 24px;
                font-weight: 700;
            }

        .content .content__grid {
            min-width: 100%;
            /* height: 100%; */
            background-color: #ffffff;
        }

        .content .grid-filter {
            justify-content: flex-end;
            padding: 16px 16px 10px;
            background-color: #ffffff;
        }

            .content .grid-filter .search {
                position: relative;
                width: 250px;
            }

                .content .grid-filter .search input {
                    width: 240px;
                }

                .content .grid-filter .search .icon-search {
                    position: absolute;
                    background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -992px -360px;
                    cursor: pointer;
                    top: 6px;
                    left: 87%;
                }

            .content .grid-filter .reload {
                background: url("../../assets/img/Sprites.64af8f61.svg") no-repeat -423px -201px;
                border: none;
                cursor: pointer;
            }

    .no-data {
        position: sticky;
        bottom: 46;
        z-index: 6;
        background-color: #fff;
        display: flex;
        min-height: 200px;
        flex-direction: column;
        padding-left: 16px;
        padding-right: 30px;
        width: 100%;
        left: 0;
        align-items: center;
        justify-content: center;
    }

        .no-data p {
            padding: 40px 0 0 0;
        }

    .content .pagination {
        position: sticky;
        bottom: 0;
        z-index: 6;
        background-color: #fff;
        display: flex;
        min-height: 46px;
        padding-left: 16px;
        padding-right: 30px;
        width: 100%;
        left: 0;
        justify-content: space-between;
    }

        .content .pagination .select {
            justify-content: space-between;
        }

            .content .pagination .select select {
                margin: 0 16px;
                width: 200px;
                min-width: 200px;
            }

    .right-5 {
        right: 5px;
    }

    .m-dropdown--menu {
        position: absolute;
        background-color: #fff;
    }
    /*  */
    ul {
        width: 125px;
        padding: 5px 0px;
    }

        ul li {
            list-style: none;
            padding: 5px 10px;
        }

            ul li:hover {
                background-color: #f3f8f8;
                color: #08bf1e;
            }

    .button-arrow {
        width: 26px;
        height: 16px;
        cursor: pointer;
    }

    .btn-edit {
        height: 36px;
        width: 40px;
        background-color: #fff;
        border: none;
        cursor: pointer;
        font-weight: 600;
        color: #7979de;
    }

        .btn-edit:hover {
            background-color: #f3f8f8;
        }

    table {
        display: grid;
        min-width: 100%;
        width: max-content;
        grid-template-columns: minmax(16px, 16px) minmax(40px, 40px) minmax(130px, 1fr) minmax(200px, 1.67fr) minmax(100px, 0.5fr) minmax(130px, 1fr) minmax( 130px, 2fr ) minmax(150px, 1.67fr) minmax(150px, 1.67fr) minmax(150px, 1.67fr) minmax( 150px, 1.67fr ) minmax(200px, 1.67fr) minmax(130px, 1fr) minmax(30px, 30px) minmax(30px, 30px);
    }

    thead,
    tbody,
    tr {
        display: contents;
        width: 100%;
    }

    table td {
        border-bottom: 1px solid #e8e8e8;
        padding: 5px 10px;
        text-align: left;
        /* overflow: hidden;
      text-overflow: ellipsis;*/
        /* white-space: nowrap; */
        align-items: center;
        display: flex;
    }

    td.sticky-right {
        border-left: 1px dotted #c7c7c7;
    }

    td {
        background-color: #fff;
        border-bottom: 1px solid #c7c7c7;
        border-right: 1px dotted #c7c7c7;
    }

    th.sticky-right {
        border-left: 1px solid #c7c7c7;
        background: #eceef1 !important;
        z-index: 5;
    }

    th.sticky-left {
        background: #eceef1 !important;
        z-index: 5;
    }

    th {
        position: sticky;
        display: flex;
        top: 74px;
        background-color: #ffffff;
        align-items: center;
        padding: 5px 10px 3px;
        min-height: 34px;
        height: auto;
        text-transform: uppercase;
        font-size: 12px;
        border-right: 1px solid #c7c7c7;
        border-bottom: 1px solid #c7c7c7;
        background: #eceef1;
    }

    /* tr:nth-child(even) td {
      background: #f6f6f6;
    } */
    td:last-child {
        display: flex;
        justify-content: center;
        cursor: pointer;
    }

    tr:hover > td:not(:first-child, :last-child, :nth-child(14), .selected),
    tr:hover > td .btn-edit {
        background: #f3f8f8 !important;
    }
</style>
