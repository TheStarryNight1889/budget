<template>
  <div class="login">
    <div
      class="register shadow-lg container my-20 mx-auto px-20 py-20 rounded-lg border-2"
    >
      <h1 class="text-left text-4xl">Login Page</h1>
      <form class="flex flex-col py-10" v-on:submit.prevent="login">
        <label class="text-left py-4" for="email">Email:</label>
        <input
          class="rounded-lg border-2 py-2 px-2"
          id="email"
          v-model="email"
          placeholder="Alex123@example.com"
        />
        <label class="text-left py-4" for="password">Password:</label>
        <input
          class="rounded-lg border-2 py-2 px-2"
          v-model="password"
          id="password"
          placeholder="supersecretpassword123"
        />
        <button
          class="p-3 shadow hover:bg-green-400 bg-green-300 w-40 my-4 rounded-lg text-white text-lg"
          type="submit"
        >
          Login
        </button>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "Login",
  data() {
    return {
      email: this.email,
      password: this.password
    };
  },
  components: {},
  methods: {
    async login() {
      try {
        var response = await this.$http.post("/Login", {
          email: this.email,
          password: this.password
        });

        if (response.status == 200) {
          this.$cookies.set("token", response.data.token);
          this.$router.push("/dashboard");
        }
      } catch (err) {
        console.log(err);
        this.error = err.message;
      }
    }
  }
};
</script>
