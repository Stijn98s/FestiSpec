<template>
    <v-container fluid fill-height>
      <v-layout flex align-center justify-center>
        <v-flex xs12 sm4>
          <div>
          <v-layout align-center justify-center>
              <h1 class="display-3 mb-5">FestiWeb</h1>
          </v-layout>
              <v-layout align-center justify-center>
                  <h1 class="title mb-5">Wachtwoord kiezen</h1>
              </v-layout>
            <v-form v-model="valid" ref="form" class="mt-5">
              <v-text-field label="Nieuw wachtwoord" v-model="newpassword" type="password"  required :rules="passwordRules">     </v-text-field>
              <v-text-field label="Herhaal nieuw wachtwoord" v-model="confirmnewpassword" required type="password"
              :rules="[
              v => !!v || 'Voer een waarde in',
              v => v === newpassword || 'De wachtwoorden moeten overeenkomen'
            ]"></v-text-field>
              <v-layout>
                  <v-btn v-if="!loading" @click="submit" block :class=" { 'indigo white--text ' : valid, disabled: !valid }">Save</v-btn>
                  <v-progress-circular v-if="loading" indeterminate color="indigo" style="display: inline-block;
    width: 100%;"></v-progress-circular>
              </v-layout>
            </v-form>
          </div>
        </v-flex>
      </v-layout>
    </v-container>
</template>

<script lang="js">

import * as axios from "axios";
import {mobileUrl} from "../services/ApiServices";

export default {
  data () {
    return {
      valid: false,

      newpassword: '',
      confirmnewpassword: '',
      loading: false,
      user: this.$route.query.user,
      token: this.$route.query.token,
        passwordRules : [
             v => /^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,30}$/.test(v) || "Wachtwoord moet minimaal 8 karakters, speciaal karakter, en een cijfer"
        ]
    }
  },
  created () {
    console.log(this.token)
  },
  methods: {
    submit () {


          let config = {
              headers: {
                  'ZUMO-API-VERSION': '2.0.0'
              }
          };
          this.loading = true;
          axios.put(`${mobileUrl}/api/Password/ResetPassword`, {
              'User': this.user,
              'Token': this.token,
              'NewPassword': this.newpassword,
              'ConfirmNewPassword': this.confirmnewpassword
          }, config).then(res => {
              this.$router.push({ name: 'login' });
                console.log(res);
              self.loading = false;

          }).catch(err => {
              this.clear();
              console.log(err);
              this.loading = false
          })


    },
    clear () {
      this.$refs.form.reset()
    }
  }
  ,   computed: {
        progress () {
            return Math.min(100, this.value.length * 10)
        },
        color () {
            return ['error', 'warning', 'success'][Math.floor(this.progress / 40)]
        }
    }
}
</script>
