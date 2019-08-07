<template>
    <v-container mt-5>
        <h1 class="display-3">
            Beschikbaarheid
        </h1>
        <v-card>
            <v-layout flex align-center justify-center>
                <v-flex xs12 sm10>
                    <v-list three-line>
                        <f-planning-item v-for="availability in availabilities" :key="availability.id"
                                         :availability="availability"/>
                    </v-list>
                </v-flex>
            </v-layout>
        </v-card>
    </v-container>
</template>

<script lang="ts">
    import {availabilityClient} from './../services/ApiServices'

    import {Component, Vue} from 'vue-property-decorator'
    import {Availability} from '@/services/ApiService'
    import FPlanningItem from '@/components/FPlanningItem.vue'

    @Component({
        name: 'PlanningPage',
        components: {
            'f-planning-item': FPlanningItem
        }
    })
    export default class PlanningPage extends Vue {
        availabilities: Availability[] = [];

        async created() {
            await this.refreshAvailability()
        }

        private async refreshAvailability() {
            const availibilities = await availabilityClient.get()
            if (availibilities) {
                this.availabilities = availibilities
            }
        }
    }
</script>
