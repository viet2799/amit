<template>
    <div class="pagination items-center">
        <div class="total-record">
            Tổng số: {{total}} bản ghi
        </div>
        <div class="select flex items-center">
            <select name="" id="" @change="onClickPage(1,$event.target.value)">
                <option value="10">10 bản ghi trên 1 trang</option>
                <option value="20" selected>20 bản ghi trên 1 trang</option>
                <option value="30">30 bản ghi trên 1 trang</option>
                <option value="40">40 bản ghi trên 1 trang</option>
                <option value="50">50 bản ghi trên 1 trang</option>
            </select>
            <button class="btn-previous mr-4"
                    @click="onClickPreviousPage"
                    :disabled="isInFirstPage">
                Trước
            </button>
            <div class="page flex">
                <div v-for="page in pages"
                     :key="page.name"
                     @click="onClickPage(page.name)"
                     :class="{ page_active: isPageActive(page.name) }"
                     class="page-number"
                     :disabled="page.isDisabled">
                    {{ page.name }}
                </div>

                <!-- <div class="page-number page-active">1</div>
                <div class="page-number">2</div> -->
            </div>
            <button class="btn-next ml-4"
                    @click="onClickNextPage"
                    :disabled="isInLastPage">
                Sau
            </button>
        </div>
    </div>
</template>

<script>
    export default {
        name: "Pagination",
        props: {
            maxVisibleButtons: {
                type: Number,
                required: false,
                default: 3,
            },
            totalPages: {
                type: Number,
                required: true,
            },
            total: {
                type: Number,
                required: true,
            },
            perPage: {
                type: Number,
                required: true,
            },
            currentPage: {
                type: Number,
                required: true,
            },
        },
        computed: {
            //trang đầu
            startPage() {
                if (this.currentPage === 1) {
                    return 1;
                }

                if (this.currentPage === this.totalPages) {
                    return this.totalPages - this.maxVisibleButtons + 1;
                }

                return this.currentPage - 1;
            },

            //Lấy trang cuối
            endPage() {
                return Math.min(
                    this.startPage + this.maxVisibleButtons - 1,
                    this.totalPages
                );
            },

            //chia tổng trang ra thành nhiều trang nhỏ
            pages() {
                const range = [];

                for (let i = this.startPage; i <= this.endPage; i += 1) {
                    range.push({
                        name: i,
                        isDisabled: i === this.currentPage,
                    });
                }

                return range;
            },
            //đang ởtrang đầu tiên hay không
            isInFirstPage() {
                return this.currentPage === 1;
            },
            //đang ở trang cuối cùng hay không
            isInLastPage() {
                return this.currentPage === this.totalPages;
            },
        },
        methods: {
            /**
             * Tới trang đầu tiên
             * CreatedBy: ntquan(14/05/2021)
             * */
            onClickFirstPage() {
                this.$emit("pagechanged", 1);
            },

            /**
            * Tới trang trước đó
            * CreatedBy: ntquan(14/05/2021)
            * */
            onClickPreviousPage() {
                this.$emit("pagechanged", this.currentPage - 1);
            },

            /**
            * Tới trang {page}
            * CreatedBy: ntquan(11/05/2021)
            * */
            onClickPage(page, size) {
                if (size) {
                    this.$store.commit("setPageSize", size)
                }
                this.$emit("pagechanged", page, size);
            },

            /**
            * Tới trang tiếp theo
            * CreatedBy: ntquan(11/05/2021)
            * */
            onClickNextPage() {
                this.$emit("pagechanged", this.currentPage + 1);
            },

            /**
            * Tới trang cuối
            * CreatedBy: ntquan(11/05/2021)
            * */
            onClickLastPage() {
                this.$emit("pagechanged", this.totalPages);
            },

            /**
             * xác định trang đang chọn
             * @param page trang
             * CreatedBy: ntquan(11/05/2021)
             */
            isPageActive(page) {
                return this.currentPage === page;
            },
        },
    };
</script>

<style>
    .pagination {
        position: absolute;
        bottom: 10px;
        z-index: 6;
        background-color: #fff;
        display: flex;
        min-height: 47px;
        padding-left: 16px;
        padding-right: 30px;
        width: 93%;
        justify-content: space-between;
    }

        .pagination .select {
            justify-content: space-between;
        }

            .pagination .select select {
                margin: 0 16px;
                width: 200px;
                min-width: 200px;
                min-height: 32px;
                border: 1px solid #babec5;
                border-radius: 2px;
                background-color: #fff;
                overflow: hidden;
            }

    .right-5 {
        right: 5px;
    }

    .m-dropdown--menu {
        position: absolute;
        background-color: #fff;
    }
</style>
