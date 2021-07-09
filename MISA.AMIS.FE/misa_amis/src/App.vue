<template>
  <AppLayout>
  </AppLayout>
  <!-- <router-view/> -->
</template>

<script>
import AppLayout from "./layouts/AppLayout";

export default {
  name: "App",
  components: {
    AppLayout,
  },
};
</script>

<style>
@import "./styles/common.css";
@import "./styles/main.css";
</style>
