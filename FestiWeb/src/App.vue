<template>
  <v-app>
    <component :is="layout" />
  </v-app>
</template>

<script>
export default {
  name: 'App',

  computed: {
    layout () {
      return this.$route.meta.layout || 'Menu'
    }
  }
}
</script>
