<template>
  <router-view>

  </router-view>
</template>

<script>
  import M from "materialize-css"
  import 'materialize-css/dist/css/materialize.css'

  export default {
    data() {
      return {
        inputValue: '',
      }
    }
  }
</script>

<style scoped>
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }

  
</style>