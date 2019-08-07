<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card class="p-2">
            <v-card-text>
                <v-form>
                    <v-radio-group v-model="answer.chosenOption" column>
                        <v-radio :label="option.value" :value="option" v-for="option in question.options"
                                 :key="option.id"></v-radio>
                    </v-radio-group>
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
import { MultipleChoiceQuestion, MultipleChoiceQuestionAnswer } from '@/services/ApiService'
import { multipleChoiceAnswerClient } from '@/services/ApiServices'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

    @Component({
      name: 'f-multiplechoice-question',
      components: { FQuestionHeader },
      props: {
        question: MultipleChoiceQuestion
      }
    })
export default class FMultipleChoiceQuestion extends Vue {
        question: MultipleChoiceQuestion | null;

        answer: MultipleChoiceQuestionAnswer = new MultipleChoiceQuestionAnswer();
        private isLoading: boolean = false;

        async created () {
          await this.refresh()
        }

        private async refresh () {
          const answer = await multipleChoiceAnswerClient.get(this.question!.id || null)
          if (answer) {
            this.answer = answer
          }
        }

        private async save () {
          try {
            this.isLoading = true
            let answer
            if (this.answer.id) {
              answer = await multipleChoiceAnswerClient.edit(this.question!.id || null, this.answer)
            } else {
              answer = await multipleChoiceAnswerClient.create(this.question!.id || null, this.answer)
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
