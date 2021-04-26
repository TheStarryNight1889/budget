<template>
  <h1>This shouldnt be displayed</h1>
</template>
<script>
export default {
  name: "Logout",
  data() {
    return {};
  },
  components: {},
  methods: {
    async logout() {
      try {
        window.$cookies.remove("token");
        this.$router.push("/login");
      } catch (err) {
        console.log(err);
        this.error = err.message;
      }
    }
  },
  mounted() {
    this.logout();
  }
};
</script>
