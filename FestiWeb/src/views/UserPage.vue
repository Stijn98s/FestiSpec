<template>
    <v-container mt-5>
        <v-layout flex align-center justify-center>
            <v-flex xs12 sm10 v-if="user">
              <v-form v-model="valid" ref="form">
                <v-layout row>
                    <v-flex xs5>
                        <v-text-field label="Voornaam" v-model="user.firstName" required
                                      :rules="[
              v => !!v || 'Voer een waarde in',
              v => (v && v.length > 2 && v.length < 45) || 'De naam moet uit minimaal 2 en maximaal 45 characters bestaan'
            ]"></v-text-field>
                    </v-flex>
                    <v-flex xs5 offset-xs1>
                        <v-text-field label="Telefoonnummer" v-model="user.phone"
                                      :rules="[
              v => !!v || 'Voer een waarde in',
              v => /(^\+[0-9]{2}|^\+[0-9]{2}\(0\)|^\(\+[0-9]{2}\)\(0\)|^00[0-9]{2}|^0)([0-9]{9}$|[0-9\-\s]{10}$)/.test(v) || 'Het ingevoerde telefoonnummer is ongeldig'
            ]"></v-text-field>
                    </v-flex>
                </v-layout>
                <v-layout row>
                    <v-flex xs5>
                        <v-text-field label="Achternaam" v-model="user.lastName" required
                                      :rules="[
              v => !!v || 'Voer een waarde in',
              v => (v && v.length > 2 && v.length < 45) || 'De naam moet uit minimaal 2 en maximaal 45 characters bestaan'
            ]"></v-text-field>
                    </v-flex>

                </v-layout>
                <v-layout row>
                    <v-flex xs5>
                        <v-text-field label="Postcode" v-model="user.postalCode" required
                                                        :rules="[
                v => !!v || 'Voer een waarde in',
                v => /^[1-9][0-9]{3}\s?(?:[a-zA-Z]{2})$/.test(v) || 'De ingevoerde postcode is ongeldig'
              ]"></v-text-field>
                    </v-flex>
                    <v-flex xs5 offset-xs1>
                        <v-select
                                v-model="gender"
                                :items="genders"
                                required
                                :rules="[
                                  v => !!v || 'Voer een waarde in'
                                ]"
                        ></v-select>
                    </v-flex>
                </v-layout>
                <v-layout row>
                    <v-flex xs5>
                        <v-text-field label="Huisnummer" v-model="user.houseNumber" required
                                      :rules="[
              v => !!v || 'Voer een waarde in',
              v => /^[1-9][0-9]{0,3}[a-zA-Z]{0,2}$/.test(v) || 'Het ingevoerde huisnummer is ongeldig'
            ]"
                        ></v-text-field>
                    </v-flex>
                    <v-flex xs5 offset-xs1>
                        <v-menu
                                ref="menu"
                                :close-on-content-click="false"
                                v-model="menu"
                                lazy
                                transition="scale-transition"
                                offset-y
                                full-width
                                min-width="290px"
                        >
                            <v-text-field label="Geboortedatum" required
                                          slot="activator"
                                          :value="user.birthDate.toLocaleDateString()"
                                          prepend-icon="event"
                                          readonly
                            ></v-text-field>
                            <v-date-picker
                                    ref="picker"
                                    :max="new Date().toISOString().substr(0, 10)"
                                    v-model="birthDate"
                            ></v-date-picker>
                        </v-menu>
                    </v-flex>

                </v-layout>
                <v-layout row>
                    <v-flex xs5>
                    </v-flex>
                    <v-layout offset-xs1 row xs5 justify-end>
                        <v-btn @click="save" :disabled="!valid || isLoading" class="indigo white--text">Save</v-btn>
                    </v-layout>
                </v-layout>
                </v-form>
            </v-flex>
        </v-layout>
    </v-container>
</template>

<script lang="ts">
import { inspectorClient } from '@/services/ApiServices'
import { Component, Vue } from 'vue-property-decorator'
import { Gender, Inspector } from '@/services/ApiService'

    @Component({
      name: 'UserPage'
    })
export default class UserPage extends Vue {
        user: Inspector = new Inspector();
        menu: boolean = false;
        genders: string[] = Object.keys(Gender).map(k => Gender[k as any]).filter(elem => isNaN(<any>elem));
        valid: boolean = true;
        private isLoading: boolean = false;

        get gender () {
          // @ts-ignore
          return Gender[this.user.gender]
        }

        set gender (value: number) {
          // @ts-ignore
          this.user.gender = Gender[value]
        }

        set birthDate (value) {
          this.user.birthDate = new Date(value)
          console.log(this.user.birthDate)
        }

        get birthDate () {
          const date = this.user.birthDate
          if (!this.user) return new Date().toISOString().substr(0, 10)
          return date!.toISOString().substr(0, 10)
        }

        async created () {
          await this.refreshUser()
        }

        private async refreshUser () {
          const user = await inspectorClient.getCurrentUser()
          if (user) {
            this.user = user
          }
        }

        private async save () {
          this.isLoading = true
          await inspectorClient.put(this.user)
          this.isLoading = false
        }
}
</script>
