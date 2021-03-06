import Vue from "vue";
import Vuex from "vuex";

Vue.use(Vuex);

const store = new Vuex.Store({
  state: {
    isToggle: false,
    isShowDialogEmployee: false,
    dataRow: {},
    pageSize: 20,
  },
  mutations: {
    toggleSideBar(state) {
      state.isToggle = !state.isToggle;
    },

    toggleDialog(state) {
      state.isShowDialogEmployee = !state.isShowDialogEmployee;
    },

    setDataRow(state, payload) {
      state.dataRow = payload;
    },

    setPageSize(state, payload) {
      state.pageSize = payload;
    },
  },
  actions: {},
  modules: {},
  getters: {
    getToggle(state) {
      return state.isToggle;
    },

    getIsShow(state) {
      return state.isShowDialogEmployee;
    },

    getDataRow(state) {
      return state.dataRow;
    },

    getPageSize(state) {
      return state.pageSize;
    },
  },
});
export default store;
