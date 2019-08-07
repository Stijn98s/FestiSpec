<template>
    <v-container mt-5>
        <v-layout flex align-center justify-center column fill-height>
            <v-flex xs12 sm10>

                <v-card class="mx-5">
                    <v-card-title primary-title>
                        <div>
                            <h3 class="headline mb-0">{{questionnaire.name}}</h3>
                            <div>{{questionnaire.description}}</div>
                        </div>
                    </v-card-title>
                </v-card>
                <v-expansion-panel popout class="my-3" v-if="questionnaire.questions">
                    <template v-for="question in sortedQuestions">
                        <f-open-question v-if="question._discriminator === 'OpenQuestion'" class="elevation-3"
                                         v-bind:key="question.id" :question="question"/>
                        <f-multiplechoice-question v-else-if="question._discriminator === 'MultipleChoiceQuestion'"
                                                   class="elevation-3" v-bind:key="question.id" :question="question"/>
                        <f-scale-question v-else-if="question._discriminator === 'ScaleQuestion'" class="elevation-3"
                                          v-bind:key="question.id" :question="question"/>
                        <f-measure-question v-else-if="question._discriminator === 'MeasureQuestion'"
                                            class="elevation-3" v-bind:key="question.id" :question="question"/>
                        <f-picture-question v-else-if="question._discriminator === 'PictureQuestion'"
                                            class="elevation-3" v-bind:key="question.id" :question="question"/>
                        <f-draw-question v-else-if="question._discriminator === 'DrawQuestion'" class="elevation-3"
                                         v-bind:key="question.id" :question="question"/>
                        <f-table-question v-else-if="question._discriminator === 'TableQuestion'" class="elevation-3"
                                          v-bind:key="question.id" :question="question"/>
                        <v-expansion-panel-content v-else-if="question._discriminator === 'CategorieQuestion'"
                                                   class="elevation-2 mt-4 indigo darken-1 white--text" :readonly="true"
                                                   v-bind:key="question.id">
                            <v-icon slot="actions" color="teal">category</v-icon>
                            <div slot="header">
                                {{question.description}}

                            </div>
                        </v-expansion-panel-content>

                    </template>
                </v-expansion-panel>


                            <v-container fluid grid-list-md>
                                <v-textarea
                                        v-model="comment"
                                        name="input-7-4"
                                        label="Commentaar"
                                        box
                                        auto-grow

                                ></v-textarea>
                                <v-btn color="success"  :disabled="isLoading" @click="saveDescription">
                                    <div v-if="!isLoading">Verstuur</div>
                                    <v-progress-circular v-else :indeterminate="true"></v-progress-circular>
                                </v-btn>
                            </v-container>

            </v-flex>
        </v-layout>
    </v-container>
</template>

<script lang="ts">
    import FOpenQuestion from '@/components/FOpenQuestion.vue'
    import FMultipleChoiceQuestion from '@/components/FMultipleChoiceQuestion.vue'
    import FScaleQuestion from '@/components/FScaleQuestion.vue'
    import FMeasureQuestion from '@/components/FMeasureQuestion.vue'

    import FPictureQuestion from '@/components/FPictureQuestion.vue'
    import FDrawQuestion from '@/components/FDrawQuestion.vue'
    import {Component, Vue} from 'vue-property-decorator'
    import {questionnaireClient} from '@/services/ApiServices'
    import {Question, Questionnaire} from '@/services/ApiService'
    import FTableQuestion from '@/components/FTableQuestion.vue'

    @Component({
        name: 'Quesionnaire',
        components: {
            'f-open-question': FOpenQuestion,
            'f-multiplechoice-question': FMultipleChoiceQuestion,
            'f-scale-question': FScaleQuestion,
            'f-measure-question': FMeasureQuestion,
            'f-draw-question': FDrawQuestion,
            'f-picture-question': FPictureQuestion,
            'f-table-question': FTableQuestion
        }
    })
    export default class QuestionnairePage extends Vue {
        questionnaire: Questionnaire = new Questionnaire();

        async created() {
            await this.refresh()
        }

        comment: string | null = "";

        private async refresh() {
            const questionnaire = await questionnaireClient.get(this.$route.params.id);
            this.comment = await questionnaireClient.getComment(this.$route.params.id);
            if (questionnaire) {
                this.questionnaire = questionnaire
            }
        }

        isLoading: boolean =false;

        async saveDescription() {
            if (this.questionnaire.id && this.comment) {
                this.isLoading = true;
                await questionnaireClient.changeComment(this.questionnaire.id, this.comment);
                this.isLoading = false;
            }
        }

        get sortedQuestions(): Question[] {
            return this.questionnaire!.questions!.sort((f1, f2) => {
                // @ts-ignore
                return f1!.order - f2!.order;
            });
        }
    }
</script>
