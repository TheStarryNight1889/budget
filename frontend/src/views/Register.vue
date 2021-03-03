<template>
  <div
    class="register shadow-lg container my-20 mx-auto px-20 py-20 rounded-lg border-2"
  >
    <h1 class="text-left text-4xl">Registration Page</h1>
    <form class="flex flex-col py-10" v-on:submit.prevent="register()">
      <label class="text-left py-4" for="name">Name:</label>
      <input
        class="rounded-lg border-2 py-2 px-2"
        id="name"
        v-model="name"
        placeholder="Alex Smith"
      />
      <label class="text-left py-4" for="email">Email:</label>
      <input
        class="rounded-lg border-2 py-2 px-2"
        id="email"
        v-model="email"
        placeholder="Alex123@example.com"
      />
      <label class="text-left py-4" for="dob">Date of Birth:</label>
      <input
        class="rounded-lg border-2 py-2 px-2"
        placeholder="12/02/1999"
        v-model="dob"
        type="date"
        id="dob"
      />
      <label class="text-left py-4" for="currency">Currency Format:</label>
      <select
        class="rounded-lg border-2 py-2 px-2"
        id="currency"
        v-model="currency"
      >
        <option value="1">Euro</option>
        <option value="2">Great British Pound</option>
        <option value="3">American Dollar</option>
      </select>
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
        Register
      </button>
    </form>
  </div>
</template>

<script>
export default {
  name: "Register",
  data() {
    return {
      name: this.name,
      email: this.email,
      dob: this.dob,
      currency: this.currency,
      password: this.password
    };
  },
  components: {},
  methods: {
    async register() {
      try {
        await this.$http
          .post("/User", {
            name: this.name,
            email: this.email,
            dob: this.dob,
            currency: this.currency,
            password: this.password
          })
          .then(response => response.data);
      } catch (err) {
        this.error = err.message;
      }
    }
  }
};
</script>
