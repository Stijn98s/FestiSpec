<template>
    <v-list-tile :to="{name: 'questionnaire', params: { id: questionnaire.id }}">
        <v-list-tile-content>
            <v-layout align-center justify-center style="width: 100%">
            <v-flex xs4>
                <v-list-tile-title>{{ questionnaire.name }}</v-list-tile-title>
                <v-list-tile-sub-title>{{ event.location }}</v-list-tile-sub-title>
            </v-flex>
            <v-flex xs4>
                <v-list-tile-title>{{ event.name }}</v-list-tile-title>
            </v-flex>
            <v-flex xs4>
                <v-list-tile-title>{{ event.startDate.toISOString().substr(0,10) }} - {{ event.endDate.toISOString().substr(0,10) }}</v-list-tile-title>
            </v-flex>
            </v-layout>
        </v-list-tile-content>
        <v-list-tile-action>
            <v-btn icon color="success">
            <v-icon>chevron_right</v-icon>
            </v-btn>
        </v-list-tile-action>
    </v-list-tile>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { Questionnaire, Event } from '@/services/ApiService'
import { eventClient } from '@/services/ApiServices'

    @Component({
      name: 'f-questionnaire-item'
    })
export default class FQuestionnaireItem extends Vue {
        @Prop() questionnaire!: Questionnaire;

        event: Event = new Event();

        async created () {
          await this.refresh()
        }

        private async refresh () {
          const event = await eventClient.get(this.questionnaire.eventId || null)

          if (event) {
            this.event = event
          }
        }
}
</script>
