<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card >
            <v-card-text>
                    <CanvasDraw v-if="!isLoading" class="text-xs-center"

                            :height="480"
                            :brushSize="3"
                            :bg-url="question.pictureUrl"
                            :answer-url="answer.imageUrl"
                            :outputName="'example'" @file-uploaded="save"/>
            </v-card-text>
        </v-card>
    </v-expansion-panel-content>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { DrawQuestion, DrawQuestionAnswer } from '@/services/ApiService'
import { drawAnswerClient } from '@/services/ApiServices'
import CanvasDraw from '@/components/CanvasDraw.vue'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

    @Component({
      name: 'f-draw-question',
      components: { CanvasDraw, FQuestionHeader }
    })
export default class FDrawQuestion extends Vue {
        @Prop() question: DrawQuestion | null;

        answer: DrawQuestionAnswer = new DrawQuestionAnswer();

        async created () {
          await this.refresh()
        }

        private isLoading:boolean = true;

        private async refresh () {
          const answer = await drawAnswerClient.get(this.question!.id || null)
          if (answer) {
            this.answer = answer
          }
          this.isLoading = false
        }

        private async save (url: string) {
          let answer
          this.answer.imageUrl = url
          if (this.answer.id) {
            answer = await drawAnswerClient.edit(this.question!.id || null, this.answer)
          } else {
            answer = await drawAnswerClient.create(this.question!.id || null, this.answer)
          }

          if (answer) {
            this.answer = answer
          }
        }
}
</script>
