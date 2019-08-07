<template>
  <v-container mt-5>

    <v-layout flex align-center justify-center>
      <v-flex xs12 sm10>
        <h1 class="display-3">
          Vragenlijsten
        </h1>

        <v-card>

        <v-list two-line>
          <f-questionnaire-item v-for="questionnaire in questionnaires" :key="questionnaire.id" :questionnaire="questionnaire"></f-questionnaire-item>
        </v-list>
      </v-card>
      </v-flex>

    </v-layout>

  </v-container>
</template>

<script lang="ts">
import { Component, Vue } from 'vue-property-decorator'
import { questionnaireClient } from '@/services/ApiServices'
import { Questionnaire } from '@/services/ApiService'
import FQuestionnaireItem from '../components/FQuestionnaireItem.vue'

  @Component({
    name: 'Quesionnaires',
    components: {
      'f-questionnaire-item': FQuestionnaireItem
    }
  })
export default class QuestionnairesPage extends Vue {
    questionnaires: Questionnaire[] = [];

    async created () {
      await this.refresh()
    }

    private async refresh () {
      const questionnaires = await questionnaireClient.getAll()

      if (questionnaires) {
        this.questionnaires = questionnaires
      }
    }
}
</script>
