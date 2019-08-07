<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card class="p-2">
            <v-card-text>
                <v-form>
                    <v-text-field :counter="50" label="Antwoord" required v-model="answer.answer"></v-text-field>
                    <v-btn color="success" v-on:click="save">
                        <div v-if="!isLoading">Verstuur</div>
                        <v-progress-circular v-else :indeterminate="true"></v-progress-circular>
                    </v-btn>
                </v-form>
            </v-card-text>
        </v-card>

    </v-expansion-panel-content>

</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { OpenQuestion, OpenQuestionAnswer } from '@/services/ApiService'
import { openAnswerClient } from '@/services/ApiServices'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

    @Component({
      props: {
        question: OpenQuestion
      },
      components: { FQuestionHeader },
      name: 'f-open-question'
    })
export default class FOpenQuestion extends Vue {
        private isLoading: boolean = false;
        private question: OpenQuestion | null;
        private answer: OpenQuestionAnswer = new OpenQuestionAnswer();

        async created () {
          await this.refresh()
        }

        private async refresh () {
          if (this.question!.id) {
            const answer = await openAnswerClient.get(this.question!.id || null)
            if (answer) {
              this.answer = answer
            }
          }
        }

        private async save () {
          try {
            this.isLoading = true
            let answer

            if (this.answer.id) {
              answer = await openAnswerClient.edit(this.question!.id || null, this.answer)
            } else {
              answer = await openAnswerClient.create(this.question!.id || null, this.answer)
            }

            if (answer) {
              this.answer = answer
            }
          } catch (e) {
            console.log(e)
          }
          this.isLoading = false
        }
}
</script>
