<template>
    <v-expansion-panel-content>
            <f-question-header slot="header" :question="question" :answer="answer"/>
            <v-card class="p-2">
                <v-card-text>
                    <v-form v-if="!isLoading">

                        <f-picture-picker @file-uploaded="handleUpload" :initial-img="answer.imageUrl" :answer="answer"
                                          ref="picturePicker"/>

                    </v-form>
                </v-card-text>
            </v-card>
    </v-expansion-panel-content>
</template>
<script lang="ts">
import { Component, Prop, Vue } from 'vue-property-decorator'
import { PictureQuestion, PictureQuestionAnswer } from '@/services/ApiService'
import { pictureAnswerClient } from '@/services/ApiServices'
import FQuestionHeader from '@/components/FQuestionHeader.vue'

import FPicturePicker from '@/components/FPicturePicker.vue'

    @Component({
      name: 'f-picture-question',
      components: { FQuestionHeader, FPicturePicker }
    })
export default class FPictureQuestion extends Vue {
        @Prop() question: PictureQuestion;

        private image: any = null;
        answer: PictureQuestionAnswer = new PictureQuestionAnswer();
        private formData: any;

        isLoading: boolean = true;

        async created () {
          await this.refresh()
        }

        private async refresh () {
          const answer = await pictureAnswerClient.get(this.question!.id || null)
          if (answer) {
            this.answer = answer
          }
          this.isLoading = false
        }

        async handleUpload (url: string) {
          console.log(url)
          this.answer.imageUrl = url
          await this.save()
        }

        private async save () {
          let answer

          if (this.answer.id) {
            answer = await pictureAnswerClient.edit(this.question!.id || null, this.answer)
          } else {
            answer = await pictureAnswerClient.create(this.question!.id || null, this.answer)
          }

          if (answer) {
            this.answer = answer
          }
        }
}
</script>
