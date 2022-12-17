<template>
    <button class="btn">
        <slot></slot>
    </button>
</template>

<script>
    export default {
        name: 'third-eye-button',
    }
</script>

<style scoped>
    .btn {
        padding: auto;
        box-sizing: border-box;
        border-width: 5px;
        border-radius: 20px;
        font-size: 16px;
        background-color: #45AD8C;
        color: #FFFFFF;
        border-color: #57FAC5;
        border-style: solid;
    }

    .btn:hover {
        transition-duration: 0.5s;
        filter: brightness(1.25);
    }

    .btn:active {
        transition-duration: 0.2s;
        transform: translate3d(2px, 2px, 2px);
    }
</style>