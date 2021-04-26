<template>
  <div>
    <div class="user">
      <h1 class="text-2xl mt-10 text-center">Your Account</h1>
      <div
        class="register shadow-lg container my-20 mt-10 mx-auto px-20 py-20 rounded-lg border-2"
      >
        <h1 class="text-left text-2xl">Hello, {{ user.name }}</h1>
        <p class="text-left py-1">Email: {{ user.email }}</p>
        <p class="text-left py-1">Date of Birth: {{ user.dob }}</p>
      </div>
    </div>
    <h1 class="text-2xl mt-10 text-center">Your Wallets</h1>
    <div class="justify-center grid grid-flow-col auto-cols-max gap-4">
      <div class="shadow-lg my-20 mt-10 mx-auto px-20 py-20 rounded-lg border-2" v-for="wallet in wallets" :key="wallet._id">
        <p>{{ wallet.name }} Wallet</p>
        <p>Balance: {{ wallet.balance }}</p>
        <p>Last Updated: {{ wallet.lastUpdated}}</p>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "Dashboard",
  data() {
    return {
      wallets: this.wallets,
      user: this.user
    };
  },
  components: {},
  methods: {
    async getWallets() {
      try {
        var token = window.$cookies.get("token");
        var response = await this.$http.get("/Wallet", {
          headers: { Authorization: "Bearer " + token }
        });
        this.wallets = response.data;
      } catch (err) {
        console.log(err);
        this.error = err.message;
      }
    },
    async getUser() {
      try {
        var token = window.$cookies.get("token");
        var response = await this.$http.get("/User", {
          headers: { Authorization: "Bearer " + token }
        });
        this.user = response.data;
      } catch (err) {
        console.log(err);
        this.error = err.message;
      }
    }
  },
  mounted() {
    this.getWallets();
    this.getUser();
  }
};
</script>
