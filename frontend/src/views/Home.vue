<template>
  <div
    class="home shadow-lg container my-20 mx-auto px-20 py-20 rounded-lg border-2"
  >
    <h1 class="text-4xl text-center text-green-400">LaBudget</h1>
    <h1 class="text-3xl text-center">
      A <span class="text-green-300">blazingly</span> fast personal finance
      tool.
    </h1>
    <div class="text-center">
      <router-link to="/register">
        <button
          class="p-3 shadow hover:bg-green-400 bg-green-300 w-40 mt-8 rounded-lg text-white text-lg"
          type="submit"
        >
          Register
        </button>
      </router-link>
    </div>
  </div>
</template>

<script>
export default {
  name: "Home",
  components: {}
};
</script>
