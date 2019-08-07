<template>
    <v-list-tile>
        <v-list-tile-content>
            <v-layout align-center justify-center style="width: 100%">
                <v-flex xs12>
                    <v-list-tile-title>{{ availability.event.name}}</v-list-tile-title>
                    <v-list-tile-sub-title>{{ availability.event.location }}</v-list-tile-sub-title>
                </v-flex>
                <v-flex xs12>
                    <v-list-tile-title>{{ availability.event.startDate.toISOString().substr(0,10) }} - {{ availability.event.endDate.toISOString().substr(0,10) }}</v-list-tile-title>
                </v-flex>
                <v-flex xs12>
                    <v-select item-text="name" :items="availableOptions" v-model="isAvailable"  item-value="value" ></v-select>
                </v-flex>
            </v-layout>
        </v-list-tile-content>
        <v-list-tile-action>
            <v-icon v-if="availability.id === null" :color="'orange'">
                error_outline
            </v-icon>

            <v-icon v-else :color="'green'">
                check_circle_outline
            </v-icon>
        </v-list-tile-action>
    </v-list-tile>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { Availability } from '@/services/ApiService'
import { availabilityClient } from '@/services/ApiServices'

    @Component({
      name: 'f-planning-item'
    })
export default class FPlanningItem extends Vue {
        @Prop() availability!: Availability;

        
        
            availableOptions: string[] = ['Beschikbaar', 'Niet beschikbaar'];
        private _isAvailable: boolean;

            get isAvailable () {
              if (!this.availability.id) {
                return null
              }
              return this.availability.isAvailable ? this.availableOptions[0] : this.availableOptions[1]
            }

            
            
            
            
            set isAvailable (available: any) {
              this._isAvailable = available == 'Beschikbaar'

              this.saveUpdate()
            }

            created () {

            }

            private async saveUpdate () {
                let availability = this.availability;
                availability.isAvailable = this._isAvailable;
              if (this.availability.id) {
                availability = await availabilityClient.edit(availability) || new Availability();
              } else {
                availability = await availabilityClient.create(availability) || new Availability();
              }
              if (availability) {
                this.availability = availability
              }
            }
}
</script>
