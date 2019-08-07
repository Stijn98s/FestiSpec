<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card class="p-2">
            <v-card-text>
                <v-text-field clearable
                        :counter="10" label="Antwoord" v-model="answer.value" required :suffix="question.unit"
                        type="number"
                              mask="##########"
                >
                </v-text-field>

                <v-form>
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
import { MeasureQuestion, MeasureQuestionAnswer } from '@/services/ApiService'
import { measureAnswerClient } from '@/services/ApiServices'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

    @Component({
      name: 'f-measure-question',
      components: { FQuestionHeader }
    })
export default class FMeasureQuestion extends Vue {
        @Prop({ default: null })
        question: MeasureQuestion | null;

        answer: MeasureQuestionAnswer = new MeasureQuestionAnswer();

        async created () {
          await this.refresh()
        }

        private isLoading: boolean = false;

        private async refresh () {
          const answer = await measureAnswerClient.get(this.question!.id || null);
          if (answer) {
            this.answer = answer
          }
        }

        private async save () {
          try {
            this.isLoading = true
            let answer
            if (this.answer.id) {
              answer = await measureAnswerClient.edit(this.question!.id || null, this.answer)
            } else {
              answer = await measureAnswerClient.create(this.question!.id || null, this.answer)
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
