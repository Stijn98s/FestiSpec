<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card class="p-2">
            <v-card-text>

                <v-form>
                    minimaal {{ question.min }} en maximaal {{ question.max }}
                    <v-slider
                            v-model="answer.value" :min="question.min" :max="question.max"
                            thumb-label
                    ></v-slider>
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
import { MultipleChoiceQuestion, ScaleQuestion, ScaleQuestionAnswer } from '@/services/ApiService'
import { scaleAnswerClient } from '@/services/ApiServices'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

    @Component({
      name: 'f-scale-question',
      components: { FQuestionHeader },
      props: {
        question: MultipleChoiceQuestion
      }
    })
export default class FScaleQuestion extends Vue {
        @Prop({ default: null }) question: ScaleQuestion | null;

        answer: ScaleQuestionAnswer = new ScaleQuestionAnswer();
        private isLoading: boolean = false;

        async created () {
          await this.refresh()
        }

        private async refresh () {
          const answer = await scaleAnswerClient.get(this.question!.id || null)
          if (answer) {
            this.answer = answer
          }
        }

        private async save () {
          try {
            this.isLoading = true
            let answer

            if (this.answer.id) {
              answer = await scaleAnswerClient.edit(this.question!.id || null, this.answer)
            } else {
              answer = await scaleAnswerClient.create(this.question!.id || null, this.answer)
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
