<template>
    <v-menu
            ref="menu"
            :close-on-content-click="false"
            v-model="menu2"
            :nudge-right="40"
            :return-value.sync="time"
            lazy
            transition="scale-transition"
            offset-y
            full-width
            max-width="290px"
            min-width="290px"
    >
        <v-text-field
                slot="activator"
                v-model="time"
                label="Tijd"
                prepend-icon="access_time"
                readonly
        ></v-text-field>
        <v-time-picker
                format="24hr"
                v-if="menu2"
                v-model="time"
                full-width

                @change="updateTime"
        ></v-time-picker>
    </v-menu>
</template>
<script lang="js">
    export default {
        name: 'f-time-component',
        props: {
            menu2: {},
            value: Date
        },
        data(){
            return{
                time: `${this.value.getHours()}:${this.value.getMinutes()}`
            }
        },
        methods:{
            updateTime(){
                let date = new Date();
                let hour = this.time.split(":");
                date.setHours(hour[0]);
                date.setMinutes(hour[1]);
                this.$emit("input", date) ;
                this.$refs.menu.save(this.time);
            }
        }
    }
</script>