<template>
  <div id="app">
    <div class="border w-full py-4 px-4 bg-green-500" id="nav">
      <router-link
        class="hover:bg-green-300 text-white text-2xl border-white border-2 rounded-lg px-2 py-1 mx-2 text-center"
        to="/"
        >Home</router-link
      >
      <router-link
        class="hover:bg-green-300 text-white text-2xl border-white border-2 rounded-lg px-2 py-1 mx-2 text-center"
        to="/about"
        >About</router-link
      >
      <div class="float-right">
        <router-link
          class="text-white hover:bg-green-300 text-2xl border-white border-2 rounded-lg px-2 py-1 mx-2 text-center"
          to="/register"
          >Register</router-link
        >
        <router-link
          class="hover:bg-green-300 text-white text-2xl border-white border-2 rounded-lg px-2 py-1 mx-2 text-center"
          to="/login"
          >Login</router-link
        >
      </div>
    </div>
    <router-view />
  </div>
</template>
