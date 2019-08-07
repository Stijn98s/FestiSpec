<template>
    <v-container fluid fill-height>
        <v-layout flex align-center justify-center>
            <v-flex xs12 sm4>
                <div>
                    <v-layout align-center justify-center>
                        <h1 class="display-3 mb-5">FestiWeb</h1>
                    </v-layout>
                    <v-layout align-center justify-center>
                        <h1 class="title mb-5">Inloggen</h1>
                    </v-layout>
                    <v-form v-model="valid" ref="form" class="mt-5">
                        <v-text-field label="Username" v-model="username" :rules="[
              v => !!v || 'Voer een waarde in'
            ]"
                        ></v-text-field>
                        <v-text-field label="Wachtwoord" v-model="password" required type="password"
                                      :rules="[
              v => !!v || 'Voer een waarde in'
            ]"></v-text-field>
                        <v-layout>
                            <v-btn v-if="!loading" @click="submit" block
                                   :class=" { 'indigo white--text ' : valid, disabled: !valid }">Login
                            </v-btn>
                            <v-progress-circular v-if="loading" indeterminate color="indigo" style="display: inline-block;
    width: 100%;"></v-progress-circular>
                        </v-layout>
                    </v-form>
                </div>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script>
import { mobileUrl } from '../services/ApiServices'
import * as axios from 'axios'

export default {
  data () {
    return {
      valid: false,
      username: '',
      password: '',
      loading: false
    }
  },
  methods: {
    submit () {
      var self = this
      let config = {
        headers: {
          'ZUMO-API-VERSION': '2.0.0'
        }
      }
      if (self.$refs.form.validate()) {
        self.loading = true
        axios.post(`${mobileUrl}/api/WebAuth`, {
          'Username': self.username,
          'Password': self.password
        }, config).then(res => {
          localStorage.setItem('token', res.data.authenticationToken)
          self.$store.commit('retrieveToken', res.data.authenticationToken)

          self.loading = true
          self.$router.push({ name: 'dashboard' })
        }).catch(err => {
          console.log(err)
          self.password = ''
          self.loading = false
        })
      }
    },
    clear () {
      this.$refs.form.reset()
    }
  }
}
</script>
