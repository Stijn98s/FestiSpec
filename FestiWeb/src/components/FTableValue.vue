<template>
    <v-flex xs12 md5>

        <v-text-field clearable v-if="value._discriminator === 'TableQuestionAnswerStringValue'"
                      :counter="10"
                      v-model="value.answerValue"

                      label="Tekst"
                      required
        ></v-text-field>
        <v-text-field clearable v-else-if="value._discriminator === 'TableQuestionAnswerNumberValue'"
                      :counter="10"
                      v-model="value.answerValue"
                      label="Getal"



                      type="number"
        ></v-text-field>

        <v-flex v-if="value._discriminator === 'TableQuestionAnswerTimeValue'" >
            <f-time-component :menu2="menu2" v-model="value.answerValue"/>
        </v-flex>

        <v-select v-else-if="value._discriminator === 'TableQuestionAnswerMultipleValue'"
                  :items="options"
                  item-value="value"
                  item-text="value"
                  return-object
                  v-model="value.answerValue"
                  label="Keuze"
        ></v-select>

    </v-flex>
</template>
<script lang="ts">


    import {Component, Prop, Vue} from 'vue-property-decorator'
    import {
        AbstractTableQuestionAnswerValue,
        IAbstractTableQuestionAnswerValue,
        MultipleChoiceQuestionOption
    } from "@/services/ApiService";
    import FTimeComponent from "@/components/FTimeComponent.vue";

    @Component({
        name: 'f-table-value',
        components: {FTimeComponent},

    })
    export default class FTableValue extends Vue {
        @Prop()
        value: AbstractTableQuestionAnswerValue;


        @Prop()
        options: MultipleChoiceQuestionOption[];

        time: Date;



        menu2: boolean = false;


        log(){
            console.log(this.time);
        }
    }
</script>
