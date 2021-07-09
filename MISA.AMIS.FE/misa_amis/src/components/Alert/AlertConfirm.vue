<template>
  <div class="alert-confirm"  @click="hideModal">
    <div class="alert-content">
      <div class="alert-header">
        <div class="alert-title">
          Thông báo
        </div>
        <div class="dialog-close-button" @click="hideModal">&#x2715;</div>
      </div>
      <div class="alert-body">
        <p>{{ alert }}</p>
      </div>
      <div class="alert-footer">
        <button class="m-btn" @click="onDelete">Đồng ý</button>
        <button class="btn-default m-btn">Huỷ bỏ</button>
      </div>
    </div>
  </div>
</template>

<script>
import { EventBus } from "../../main";

export default {
  name: "DialogConfirm",
  data() {
    return {
      isShow: false ,
      alert: "",
    };
  },

  methods: {
    hideModal() {
      this.isShow = false;
    },

    onConfirm() {
      this.$emit("onConfirm");
    },

    onDelete() {
       EventBus.$emit("isConfirm");
    },
  },

  created() {
    var vm = this;
    EventBus.$on("onFilter", (data) => {
      console.log(data);
      vm.isShow = data.isShow;
      vm.alert = data.alert;
    });
  },

  beforeDestroy() {
    EventBus.$off("onFilter");
  },
};
</script>

<style>
.alert-confirm {
  position: fixed;
  top: 0;
  left: 0;
  /* right: 0; */
  /* bottom: 0; */
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  max-height: 100%;
  overflow: auto;
  /* z-index: 2; */
  display: flex;
  justify-content: center;
  align-items: center;
}

.alert-confirm .alert-content {
  position: absolute;
  width: 500px;
  height: 200px;
  background-color: azure;
  padding: 15px 10px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.alert-confirm .alert-content .alert-header {
  height: 50px;
  display: flex;
  justify-content: space-between;
}

.alert-confirm .alert-content .alert-footer {
  height: 50px;
  display: flex;
  justify-content: space-between;
}
</style>
