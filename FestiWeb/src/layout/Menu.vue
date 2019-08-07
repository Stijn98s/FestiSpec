<template>
  <v-content>
    <v-navigation-drawer v-model="drawer" fixed app>
      <v-list dense>
        <v-list-tile :to="{name: 'dashboard'}" @click="drawer = false" exact>
          <v-list-tile-action>
            <v-icon>home</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Dashboard</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>

        <v-list-tile :to="{name: 'planning'}" @click="drawer = false" exact>
          <v-list-tile-action>
            <v-icon>schedule</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Planning</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>

        <v-list-tile :to="{name: 'questionnaires'}" @click="drawer = false" exact>
          <v-list-tile-action>
            <v-icon>assignment</v-icon>
          </v-list-tile-action>
          <v-list-tile-content>
            <v-list-tile-title>Vragenlijsten</v-list-tile-title>
          </v-list-tile-content>
        </v-list-tile>
      </v-list>
    </v-navigation-drawer>
    <v-toolbar color="indigo" dark fixed app>
      <v-toolbar-side-icon @click.stop="drawer = !drawer"></v-toolbar-side-icon>
      <v-toolbar-title>FestiWeb</v-toolbar-title>

      <v-spacer></v-spacer>
      <v-menu bottom left>
        <v-btn icon slot="activator">
          <v-icon>more_vert</v-icon>
        </v-btn>
        <v-list>
          <v-list-tile :to="{name: 'user'}" @click="drawer = false">
            <v-list-tile-action>
            <v-icon>account_box</v-icon>
          </v-list-tile-action>
            <v-list-tile-title>Profiel</v-list-tile-title>
          </v-list-tile>
          <v-list-tile @click="logout">
            <v-list-tile-action>
            <v-icon>exit_to_app</v-icon>
          </v-list-tile-action>
            <v-list-tile-title>Uitloggen</v-list-tile-title>
          </v-list-tile>
        </v-list>
      </v-menu>
    </v-toolbar>
      <router-view></router-view>
  </v-content>
</template>

<script>
export default {
  name: 'Menu',

  data: () => ({
    drawer: false
  }),

  methods: {
    logout () {
      this.$store.commit('destroyToken')
      this.$router.push({ name: 'login' })
    }
  }
}
</script>
