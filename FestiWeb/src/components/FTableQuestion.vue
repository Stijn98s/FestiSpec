<template>
    <v-expansion-panel-content>
        <f-question-header slot="header" :question="question" :answer="answer"/>
        <v-card class="p-2">
            <v-form>
                <v-container>
                    <v-layout class="title">
                        <v-flex row xs12>
                            {{question.keyColumn.header}}
                        </v-flex>
                        <v-flex row xs12>
                            {{question.valueColumn.header}}
                        </v-flex>
                    </v-layout>
                    <v-layout v-for="(answerEntry, index) in answerEntries" :key="index">
                        <f-table-value v-model="answerEntry.key" :options="question.valueColumn.options"/>
                        <v-spacer></v-spacer>
                        <f-table-value v-model="answerEntry.value" :options="question.valueColumn.options"/>
                        <v-btn icon class="mt-4" :disabled="answerEntries.length <= 1" color="error" md1 v-on:click="answerEntries.splice(index, 1)">X</v-btn>
                    </v-layout>
                    <v-btn  color="primary" v-on:click="addAnswerEntry">
                        <div>Rij toevoegen</div>
                    </v-btn>
                    <v-btn color="success"  :disabled="!canSend()" v-on:click="save">
                        <div v-if="!isLoading">Verstuur</div>
                        <v-progress-circular v-else :indeterminate="true"></v-progress-circular>
                    </v-btn>
                </v-container>

            </v-form>
        </v-card>
    </v-expansion-panel-content>
</template>
<script lang="ts">
    import {Component, Vue} from 'vue-property-decorator'
    import {
        MultipleChoiceQuestionAnswer,
        TableQuestion,
        TableQuestionAnswer, TableQuestionAnswerEntry
    } from '@/services/ApiService'
    import {tableAnswerClient} from '@/services/ApiServices'
    import FQuestionHeader from '@/components/FQuestionHeader.vue'
    import {AnswerEntryFactory} from '@/factories/tableQuestionFactory'
    import FTableValue from "@/components/FTableValue.vue";

    @Component({
        name: 'f-table-question',
        components: {FTableValue, FQuestionHeader},
        props: {
            question: TableQuestion
        }
    })
    export default class FMultipleChoiceQuestion extends Vue {
        question: TableQuestion | null;
        answerEntries: TableQuestionAnswerEntry[] = [];
        answer: TableQuestionAnswer = new TableQuestionAnswer();
        private isLoading: boolean = false;

        async created() {
            await this.refresh();
            if (!this.answer.answerEntries) this.answer.answerEntries = [];

        }

        private async refresh() {
            const answer = await tableAnswerClient.get(this.question!.id || null);
            if (answer) {
                this.answer = answer;
                this.answerEntries = this.answer.answerEntries || [];
                console.log(this.answer);
            }

            if(this.answerEntries.length < 1) this.addAnswerEntry();


        }

        private canSend(): boolean{
            return this.answerEntries.every(el => {
                //@ts-ignore
                return (<any>el.value.answerValue) && (<any>el.key.answerValue);

            });
        }

        private async save() {
            try {
                this.isLoading = true;
                let answer;
                this.answer.answerEntries = this.answerEntries;
                //@ts-ignore
                console.log(this.answer.answerEntries[0].value.answerValue);

                if (this.answer.id) {
                    console.log(this.answer);
                    answer = await tableAnswerClient.edit(this.question!.id || null, this.answer)
                } else {
                    answer = await tableAnswerClient.create(this.question!.id || null, this.answer)
                }

                if (answer) {
                    this.answer = answer
                }
            } catch (e) {
                console.log(e)
            }
            this.isLoading = false
        }

        private addAnswerEntry() {

            if (this.question) this.answerEntries.push(AnswerEntryFactory.getAnswerEntry(this.question));
        }
    }
</script>
