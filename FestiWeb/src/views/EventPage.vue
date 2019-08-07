<template>
  <v-container mt-5>
    <v-layout flex align-center justify-center>
      <v-flex xs12 sm10>
        <v-layout  row>
          <v-flex xs6>
            <p class="mb-1">Naam</p>
            <p class="mb-1">Locatie</p>
            <p class="mb-1">Van</p>
            <p class="mb-1">Tot</p>
          </v-flex>
          <v-flex xs6>
            <p class="mb-1">{{ event.name }}</p>
            <p class="mb-1">{{ event.location }}</p>
            <p class="mb-1">{{ event.startDate.toISOString().substr(0,10) }}</p>
            <p class="mb-1">{{ event.endDate.toISOString().substr(0,10) }}</p>
          </v-flex>
        </v-layout>

        <v-list subheader class="mt-3">
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
      </v-flex>
    </v-layout>
  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { eventClient } from '@/services/ApiServices'
import { Event, Questionnaire } from '@/services/ApiService'

  @Component({
    name: 'Event'
  })
export default class EventPage extends Vue {
    event: Event = new Event();
    questionnaires: Questionnaire[] = [];

    async created () {
      await this.refresh()
    }

    private async refresh () {
      const event = await eventClient.get(this.$route.params.id)

      if (event) {
        this.event = event
        const questionnaires = await eventClient.getQuestionnaires(this.event.id || null)

        if (questionnaires) {
          this.questionnaires = questionnaires
        }
      }
    }
}
</script>
