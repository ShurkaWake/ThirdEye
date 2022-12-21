<template>
    <div class="row">
        <div class="input-field col s12">
            <input ref="inputbox" class="inp" 
                   :type='type' 
                   :class='class' 
                   :value='modelValue'
                   @input='updateInput'
            >
            <label for="inp" @click="setFocus">
                {{holder}}
            </label>
            <slot/>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'third-eye-input',
        props: {
            modelValue: [String, Number],
            type: String,
            class: String,
            holder: String,
        },
        methods: {
            updateInput(event){
                this.$emit('update:modelValue', event.target.value)
            },
            setFocus(){
                this.$refs.inputbox.focus();
            }
        }
    }
</script>

<style scoped>
</style>