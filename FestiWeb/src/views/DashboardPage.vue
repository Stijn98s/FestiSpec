<template>
  <v-container fluid fill-height>
    <v-layout flex align-center justify-center>
      <v-layout row>
        <v-flex xs12 sm4 offset-sm2>
          <v-card>
            <v-toolbar class="elevation-0">
              <v-icon>calendar_today</v-icon>
              <v-toolbar-title>Jouw events</v-toolbar-title>
            </v-toolbar>

            <v-list three-line style="max-height: 500px; min-height: 500px" class="scroll-y">
              <v-divider></v-divider>
              <template v-for="event in events" >
                <v-list-tile :key="event.id + 'ss'" :to="{name: 'event', params: { id: event.id }}">
                  <v-list-tile-content>
                    <v-list-tile-title>{{ event.name }}</v-list-tile-title>
                    <v-list-tile-sub-title>{{ event.startDate.toISOString().substr(0,10) }} - {{ event.endDate.toISOString().substr(0,10) }}</v-list-tile-sub-title>
                  </v-list-tile-content>
                </v-list-tile>
                <v-divider :key="event.id"></v-divider>
              </template>
            </v-list>
          </v-card>
        </v-flex>
        <v-flex v-if="events.length > 0" xs12 sm4>
          <v-card>
            <v-toolbar class="elevation-0">
              <v-icon>insert_invitation</v-icon>
              <v-toolbar-title>Huidig event</v-toolbar-title>
            </v-toolbar>
            <v-list style="max-height: 500px; min-height: 500px">
              <v-subheader>Informatie</v-subheader>

              <v-list-tile>
                <v-list-tile-content>
                  <v-list-tile-sub-title>{{ events[0].name }}</v-list-tile-sub-title>
                  <v-list-tile-sub-title>{{ events[0].location }}</v-list-tile-sub-title>
                </v-list-tile-content>
              </v-list-tile>

              <v-subheader>Vragenlijsten</v-subheader>

              <template v-for="questionnaire in questionnaires">
                <v-list-tile :to="{name: 'questionnaire', params: { id: questionnaire.id }}" :key="questionnaire.id">
                  <v-list-tile-content>
                    <v-list-tile-title>{{ questionnaire.name }}</v-list-tile-title>
                  </v-list-tile-content>
                  <v-list-tile-action>
                    <v-icon>
                      chevron_right
                    </v-icon>
                  </v-list-tile-action>
                </v-list-tile>
              </template>
            </v-list>
          </v-card>
        </v-flex>
      </v-layout>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { eventClient } from '@/services/ApiServices'
import { Event, Questionnaire } from '@/services/ApiService'

  @Component({
    name: 'Dashboard'
  })
export default class DashboardPage extends Vue {
    events: Event[] = [];
    questionnaires: Questionnaire[] = [];

    constructor () {
      super()
      this.refresh()
    }
    private async refresh () {
      const events = await eventClient.getAll()

      if (events && events.length > 0) {
        this.events = events
        const questionnaires = await eventClient.getQuestionnaires(this.events[0].id || null)

        if (questionnaires) {
          this.questionnaires = questionnaires
        }
      }
    }
}
</script>
