<template>
    <div class="page">
        <div class="message-window">
            <div class="message">{{$route.params.message}}</div>
        </div>
    </div>
</template>

<script>
    export default {
        methods: {
            onClick(){
                close()
            }
        }
    }
</script>

<style scoped>
    .page {
        top: 0;
        bottom: 0;
        right: 0;
        left: 0;
        background-color: #EAE0F5;
        position: fixed;
        display: flex;
        justify-content: center;
        align-items: center;
    }

    .message {
        text-align: center;
        font-family: sans-serif;
        font-size: 20px;
        text-overflow: ellipsis;
        word-wrap: break-word;

        background-color: white;
        padding: 25px;
        border-radius: 25px;
        min-height: 20px;
        width: 300px;
    }
</style>